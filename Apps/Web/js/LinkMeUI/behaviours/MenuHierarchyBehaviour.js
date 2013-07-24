//========================
// MenuHierarchyBehaviour
//========================

// --------
// Requires
// --------
// Javascript.aspx
// LinkMeUI.LocateHelper
// prototype.js
// scriptaculous.js (for the scrolling)

// -----
// Usage
// -----
// someElement.menuHierarchyBehaviour =
//   new MenuHierarchyBehaviour(element someElement,             // Root menu-panel
//                              string listClassName,            // Class of menu-item containers
//                              string itemClassName,            // Class of menu-items
//                              string highlightedItemClassName, // Cosmetic class to be applied to 
//                                                               // highlighted menu-items.
//                              string hoveredItemClassName,     // Cosmetics: hovered menu-items.
//                              string highlightedHoveredItemClassName, // Cosmetics: both combined.
//                              string closeButtonClassName,     // Class of items to be made to 
//                                                               // close their menu-panel on-click
//                              null,     // Internal use only (reference to parent Behaviour)
//                              null      // Internal use only (reference to "parent" item, i.e. what 
//                                        //           is to be clicked to show this Behaviour's panel)
// );
//
//
// -------
// Purpose
// -------
// To allow fairly arbitrary sets of elements to behave like vertically
// laid-out hierarchical menus.
//
// (Just a little modification will allow this Behaviour to be suitable for
// ordinary desktop-style menus too. TODO: package up constructor parameters as
// a {dictionary: "object", to: "allow extensibility"}.)
//
//
// ----------------
// What does it do?
// ----------------
// • Adds/removes class for "highlighted" state of menu item with submenu, if
//   that item's submenu is open/closed
// • Shows/hides all appropriate panels when user clicks an item with a submenu
// • Sets Y-position of submenu (using CSS top attribute)
// • Handles keyboard navigation with left/right arrow keys, if the items contain a
//   focusable element (i.e. the <a>, <button>, <input>, <select>, <textarea> tags)
// • Sets the focused element in a smart way when submenus are opened or closed.
// • Gives elements with a certain class "close panel" functionality.
//
//
//
// -------------------
// What doesn't it do?
// -------------------
// • It doesn't lay out the menus for you - place each 'level' yourself in CSS.
// • It won't affect any elements without the class names you specified. So really
//   complicated mega-menu panels are OK :)
// • In fact it does virtually no presentation work at all, which makes it extensible
//   to all sorts of menus.
//
//
//
// -------------
// What do I do?
// -------------
// This behaviour uses the "for" attribute to define the hierarchy. So:
// • Each panel needs an ID attribute
// • Each item that opens a panel when clicked needs a "for" attribute set to the ID
//   attribute of the corresponding panel.
// • Add style="display:none" to all panels except the root one, to avoid a flash
//   of all panels on page-load.
//
//
//
// --------------------
// What shouldn't I do?
// --------------------
// • Putting panels inside eachother does NOT represent a "parent-child"
//   relationship as far as this Behaviour is concerned.
// • Don't give panels their own special class for this Behaviour. They don't need it.
//   This Behaviour just follows the "for" attributes to recognise what's a panel and
//   what's not.
// • Don't set up circular relations, I expect this will cause the script to freeze
//   under many circumstances.
//
//
//
// ------
// Jargon
// ------
//
//
// Behaviour
// ¯¯¯¯¯¯¯¯¯
// This class. You attach it to the first menu-panel, it finds the others and adds itself to those too.
//
//
// Panel
// ¯¯¯¯¯
// Element that shows/hides/moves in response to actions on its 'parent' menu item
//
//
// List
// ¯¯¯¯
// Element whose immediate children are items
//
//
// Item
// ¯¯¯¯
// Element which makes panels show/hide/move in response to user actions.
//

LinkMeUI.MenuHierarchyBehaviour = Class.create({

	initialize : function(params) {
	
		// Copy constructor parameters to this Behaviour object.
		this.panel = params.elementToBehave;
		this.listClassName = params.listClassName;
		this.itemClassName = params.itemClassName;
		this.highlightedItemClassName = params.highlightedItemClassName;
		this.hoveredItemClassName = params.hoveredItemClassName;
		this.highlightedHoveredItemClassName = params.highlightedHoveredItemClassName;
		this.closeButtonClassName = params.closeButtonClassName;
		
		this.childBehaviours = new Array();
		this.childItems = new Array();
		this.childPanels = new Array();
		
		this.parentBehaviour = params.parentBehaviour;
		this.parentItem = params.parentItem;
		
		this.selectedItem = null;
		
		// Get the list associated to this Behaviour's panel.
		this.list = params.elementToBehave.down("."+this.listClassName);
		if (!this.list) {
			throw "MenuHierarchyBehaviour: Could not find element of class '" +
			this.listClassName + "' below element ID '" + this.panel.id + "'.";
		}
		
		// (The list's direct children are presumed to be items.)
		// For this behaviour's list, give each item's related panel MenuHierarchyBehaviour too.
		
		this.list.childElements().each((function(childItem) {
		// this: the Behaviour.

			// Don't process children without the requisite className.
			if (!childItem.hasClassName(this.itemClassName)) return;
			
			// Maintain a list of child items for this Behaviour's panel's list.
			this.childItems.push(childItem);
			
			// Get related panel for this item.
			var childPanelID = LinkMeUI.LocateHelper.GetForAttribute(childItem);
			var childPanel = $(childPanelID);
			
			
			// Items without panels (invalid "for" attributes fail silently, as if there was no child.)
			if (!childPanel) {
			
				// (Very) incomplete control via the keyboard
				childItem.observe('keydown', (function(event) {
					// 37 is LEFT
					if (event.keyCode == 37) {
						if (this.parentBehaviour) this.parentBehaviour.closeSubmenu(this);
					}
				}).bindAsEventListener(this));
				
			
			// Items with panels
			} else {
			
				// Maintain a list of child panels for this Behaviour.
				this.childPanels.push(childPanel);
				
				// Add Behaviour to this item's related panel.
				var childBehaviour = new LinkMeUI.MenuHierarchyBehaviour({
				                       elementToBehave:                 childPanel,
				                       listClassName:                   this.listClassName,
				                       itemClassName:                   this.itemClassName,
				                       highlightedItemClassName:        this.highlightedItemClassName,
				                       hoveredItemClassName:            this.hoveredItemClassName,
				                       highlightedHoveredItemClassName: this.highlightedHoveredItemClassName,
				                       closeButtonClassName:            this.closeButtonClassName,
				                       parentBehaviour:                 this,
				                       parentItem:                      childItem
				                     });                    
				childPanel.menuHierarchyBehaviour = childBehaviour;
				
				// Maintain a list of child Behaviours for this Behaviour.
				this.childBehaviours.push(childBehaviour);
				
				// On close click for this child panel, close this child panel
				childBehaviour.panel.select('.'+this.closeButtonClassName).each((function(childCloseButton) {
					childCloseButton.observe('click', (function(event,childBehaviour) {
						this.closeSubmenu(childBehaviour);
					}).bindAsEventListener(this,childBehaviour));
				}).bind(this));

				
				// On item click, switch to related panel.
				childItem.observe('click', (function(event, childBehaviour, childItem) {
					// this: the Behaviour.
					
					// If that was actually an input click, don't toggle the submenu off ('on' is okay though).
					var thatWasntMeantForMe = event.findElement('input') ? true : false;
					
					// Toggle submenu
					if (this.selectedItem == childItem && !childBehaviour.selectedItem && !thatWasntMeantForMe) {
						this.closeSubmenu(childBehaviour);
					} else {
						this.openSubmenu(childBehaviour);
						this.focusItem(childItem);
					}
				}).bindAsEventListener(this, childBehaviour, childItem));
				
				// On item mouseover, hover-highlight this item
				childItem.observe('mouseover', (function(event, childBehaviour, childItem) {
					// this: the behaviour
					
					if (childItem.hasClassName(this.highlightedItemClassName)) {
						childItem.removeClassName(this.highlightedItemClassName);
						childItem.addClassName(this.highlightedHoveredItemClassName);
					} else {
						childItem.addClassName(this.hoveredItemClassName);
					}
				}).bindAsEventListener(this, childBehaviour, childItem));
				
				// On item mouseover, remove hover-highlight from this item
				childItem.observe('mouseout', (function(event, childBehaviour, childItem) {
					// this: the behaviour
					
					if (childItem.hasClassName(this.highlightedHoveredItemClassName)) {
						childItem.removeClassName(this.highlightedHoveredItemClassName);
						childItem.addClassName(this.highlightedItemClassName);
					} else {
						childItem.removeClassName(this.hoveredItemClassName);
					}
				}).bindAsEventListener(this, childBehaviour, childItem));
				
				// (Very) incomplete control via the keyboard
				childItem.observe('keydown', (function(event, childBehaviour) {
					// this: the Behaviour
					
					// 39 is RIGHT
					if (event.keyCode == 39) {
						this.openSubmenu(childBehaviour);
						childBehaviour.focusFirstItem();
					}
					// 37 is LEFT
					if (event.keyCode == 37) {
						if (this.selectedItem == childItem) {
							this.closeSubmenu(childBehaviour);
						} else if (this.parentBehaviour) {
							this.parentBehaviour.closeSubmenu(this);
						}
					}
					
				}).bindAsEventListener(this, childBehaviour));
			}
			
		}).bind(this));
	},
	
	
	
	// Perform complete "open this submenu" action.
	// (Pass in the relevant child Behaviour.)
	//
	openSubmenu : function(behaviour) {
		var panel = behaviour.panel;
		var item = behaviour.parentItem;
		
		this.highlightOnlyThisItemRecursive(panel, item);
		this.setSelectedOnlyThisItemRecursive(item);
		this.showOnlyThisChildPanel(panel, item);
	},
	
	
	
	// Perform complete "close this submenu" action.
	// (Pass in the relevant child Behaviour.)
	//
	closeSubmenu : function(behaviour) {
		var panel = behaviour.panel;
		var item = behaviour.parentItem;
		
		this.highlightOnlyThisItemRecursive(panel, null);
		this.setSelectedOnlyThisItemRecursive(null);
		this.showOnlyThisChildPanel(null, item);
		this.focusItem(item);
	},
	
	
	
	// Focuses the first item (well, its first interactive control, if it has one)
	//
	focusFirstItem : function() {
		var item = this.childItems[0];
		if (!item) return;
		this.focusItem(item);
	},
	
	
	
	// Focuses a specific item (in similar manner to the above)
	//
	focusItem : function(item) {
		var element = item.select('input, a, button, select, textarea')[0];
		if (element) {
			element.focus();
		}
	},
	
	
	
	// Hide this Behaviour's panel, and ask the same of child Behaviours.
	//
	hidePanelAndChildPanels : function() {
		this.panel.hide();
		
		this.childBehaviours.each((function(childBehaviour) {
			childBehaviour.hidePanelAndChildPanels();
		}).bind(this));
	},
	
	
	
	// Hide child Behaviours' panels only
	//
	hideChildPanels : function() {
		this.childBehaviours.each((function(childBehaviour) {
			childBehaviour.hidePanelAndChildPanels();
		}).bind(this));
	},
	
	
	
	// Set the "selected" behaviour attribute to true for only one descendant item
	// (e.g. unhighlight all other child items, grandchild items etc.)
	//
	setSelectedOnlyThisItemRecursive : function(itemToSetSelected) {
		this.selectedItem = itemToSetSelected;
		
		// For each child Behaviour, ask it to unhighlight its child items too.
		this.childBehaviours.each((function(childBehaviour) {
			childBehaviour.setSelectedOnlyThisItemRecursive(null);
		}).bind(this));
	},
	
	
	
	// Highlight only one descendant item (e.g. unhighlight all other child items, grandchild items etc.)
	//
	highlightOnlyThisItemRecursive : function(panel, itemToHighlight) {
		this.highlightOnlyThisItem(itemToHighlight);
		
		// For each child Behaviour, ask it to unhighlight its child items too.
		this.childBehaviours.each((function(childBehaviour) {
			childBehaviour.highlightOnlyThisItemRecursive(childBehaviour.panel, null);
		}).bind(this));
	},
	
	
	
	// Highlight only one child item.
	//
	highlightOnlyThisItem : function(itemToHighlight) {
		// For each child item, remove the highlighted-item class.
		this.childItems.each((function(childItem) {
			if (childItem.hasClassName(this.highlightedItemClassName)) {
				childItem.removeClassName(this.highlightedItemClassName);
			}
			if (childItem.hasClassName(this.highlightedHoveredItemClassName)) {
				childItem.removeClassName(this.highlightedHoveredItemClassName);
			}
		}).bind(this));
		
		// No item to highlight => abort here.
		if (!itemToHighlight) return;
		
		// Add the highlighted-item class to itemToHighlight.
		if (itemToHighlight.hasClassName(this.hoveredItemClassName)) {
			itemToHighlight.removeClassName(this.hoveredItemClassName);
			itemToHighlight.addClassName(this.highlightedHoveredItemClassName);
		} else {
			itemToHighlight.addClassName(this.highlightedItemClassName);
		}
	},
	
	
	
	// Intuitively: opens any menu panel, by panel ID
	//
	// Technically: calls openSubMenu for each behaviour on the path to the desired panel.
	// Passing no panel ID is OK (all children hide).
	//
	// Bubbles event up to top-most parent if necessary.
	//
	openMenuUpToPanel : function(panelToShowId, scrollToIt) {
		if (this.parentBehaviour) { 
			this.parentBehaviour.openMenuUpToPanel(panelToShowId, scrollToIt);
			return;
		}
		
		if (!panelToShowId) {
			this.showOnlyThisChildPanel(null,null);
			if (scrollToIt) Effect.ScrollTo(this.panel,{offset:-70});
		} else {
			var currentBehaviour = $(panelToShowId).menuHierarchyBehaviour; // Reference to behaviour hanging off panel is a nasty hack
			var behaviourStack = new Array();
			while (currentBehaviour != null) {
				behaviourStack.push(currentBehaviour);
				currentBehaviour = currentBehaviour.parentBehaviour;
			}
			behaviourStack.pop();	// Invocations are on behaviours' parents
			while (behaviourStack.length>0) {
				currentBehaviour = behaviourStack.pop();
				currentBehaviour.parentBehaviour.openSubmenu(currentBehaviour);
			}
			if (scrollToIt) Effect.ScrollTo(panelToShowId,{offset:-70});
		}
		
	},
	
	
	
	// Show only one child panel.
	//
	// Passing no panel is OK.
	//
	showOnlyThisChildPanel : function(panelToShow, itemToPositionBy) {
	
		// For each child Behaviour, ask it to hide its own panel, and its child panels.
		// (If the child Behaviour belongs to the panel to show, just hide its child panels.)
		this.childBehaviours.each((function(childBehaviour) {
			if (childBehaviour.panel != panelToShow) {
				childBehaviour.hidePanelAndChildPanels();
			} else {
				childBehaviour.hideChildPanels();
			}
		}).bind(this));
		
		
		// No panel to show => hide all panels, then finish
		if (!panelToShow) return;
		
		panelToShow.show();
		
		// Undo any previous top-to-marginTop swapping
		panelToShow.setStyle({top: panelToShow.getStyle('margin-top')});
		panelToShow.setStyle({marginTop: '0px'});
		
		// Match y-position to top of itemToPositionBy
		panelToShow.absolutize();
		panelToShow.clonePosition(itemToPositionBy,
			{setTop: true, setLeft: false, setWidth: false, setHeight: false}
		);
		panelToShow.relativize();
		
		// Replace "top" with "margin-top" so any container of this panel
		// expands taking into account the vertical offset.
		panelToShow.setStyle({marginTop: panelToShow.getStyle('top')});
		panelToShow.setStyle({top: '0px'});
	}
});
