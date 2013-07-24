//==============================
// CheckboxesHierarchyBehaviour
//==============================

// --------
// Requires
// --------
// Javascript.aspx
// LinkMeUI.LocateHelper
// prototype.js
//
//
//
// -----
// Usage
// -----
// someElement.checkboxesHierarchyBehaviour =
//   new CheckboxesHierarchyBehaviour(element someElement, string checkboxClassName, null, null);
//
//
//
// -------
// Purpose
// -------
// To allow ordinary checkboxes to behave like a tree structure, like the
// ones you find in many program installers (tick the higher-up category to
// tick all its children).
//
//
// 
// ----------------
// What does it do?
// ----------------
// • Ticks all child checkboxes if you tick the parent
// • Clears all child checkboxes if you clear the parent
// • Updates parent checkbox state when a child is ticked/unticked
// • Supports showing a 'parent' checkbox in the 'partially checked'
//   state (appears semi-transparent).
//
//
//
// -------------------
// What doesn't it do?
// -------------------
// • It doesn't affect the styling or position of checkboxes at all. That's not the
//   job of a 'behaviour'.
//
//
// -------------
// What do I do?
// -------------
// This behaviour uses the "for" attribute to define the hierarchy, and container
// elements to group siblings (e.g. an ordinary <div>). So:
// • Each "checkboxes container" needs an ID attribute
// • Each 'parent' checkbox needs a "for" attribute set to the ID attribute of the
//   corresponding 'checkboxes container' element.
//
//
// --------------------
// What shouldn't I do?
// --------------------
// • Don't put containers-in-containers to represent the parent-child relationship -
//   because this Behaviour pulls checkboxes *any* level below the container, it will
//   probably get quite slow and/or exhibit strange behaviour when e.g. a checkbox's
//   parent is also its sibling.
// • Don't set up circular relations, I expect this will cause the script to freeze
//   under many circumstances.
//
//
//

LinkMeUI.CheckboxesHierarchyBehaviour = Class.create({

	initialize : function(checkboxContainerElement, checkboxClassName, parentCheckbox, parentCheckboxContainerBehaviour) {
		// Copy constructor parameters to this Behaviour object.
		this.checkboxContainer = checkboxContainerElement;
		this.checkboxClassName = checkboxClassName;
		this.parentCheckbox = parentCheckbox;
		this.parentCheckboxContainerBehaviour = parentCheckboxContainerBehaviour;
		
		this.childCheckboxContainers = new Array();
		this.childCheckboxes = new Array();
		this.childBehaviours = new Array();
		
		// A checkboxContainer has many "checkbox" children.
		// Iterate  these, following their "for" attributes to add this behaviour to
		// further checkboxContainers.
		//
		// (Iterate through all child checkboxes.)
		// Give each related ("child") checkboxContainer this Behaviour too.
		//
		this.checkboxContainer.select('.'+checkboxClassName).each((function(checkbox) {
		// this: the Behaviour.
			
			// Maintain a list of child checkboxes for this Behaviour.
			this.childCheckboxes.push(checkbox);
			
			// Get related ("child") checkboxContainer for this checkbox.
			var childCheckboxContainer = $(LinkMeUI.LocateHelper.GetForAttribute(checkbox));
			
			// Checkboxes without children:
			// 
			if (!childCheckboxContainer) {
				checkbox.observe('click', (function(event) {
					this.updateAncestorCheckboxes();
				}).bindAsEventListener(this));
			
			// Checkboxes with children:
			// - Give it special click behaviour.
			// - Propagate behaviour to it.
			} else {
				childCheckboxContainer.checkboxesHierarchyBehaviour =
					new LinkMeUI.CheckboxesHierarchyBehaviour(childCheckboxContainer, checkboxClassName, checkbox, this);

				// On checkbox click, propagate state down the hierarchy.
				checkbox.observe('click', (function(event) {
					// this: the Behaviour.
					this.checkDescendantCheckboxesInContainers(checkbox, childCheckboxContainer.checkboxesHierarchyBehaviour);
					this.updateAncestorCheckboxes();
				}).bindAsEventListener(this, childCheckboxContainer.checkboxesHierarchyBehaviour));
			
				// Maintain a list of child checkboxContainers
				this.childCheckboxContainers.push(childCheckboxContainer);
				
				// Maintain a list of child Behaviours for this Behaviour.
				this.childBehaviours.push(childCheckboxContainer.checkboxesHierarchyBehaviour);
			}
			
		}).bind(this));
		
		this.updateAllAggregateCheckboxes(this);
	},
	
	
	
	// Update all checkboxes with children to the correct (ternary) state.
	//
	updateAllAggregateCheckboxes : function(checkboxContainerBehaviour) {
		checkboxContainerBehaviour.childBehaviours.each((function(childBehaviour) {
			childBehaviour.updateAllAggregateCheckboxes(childBehaviour);
		}).bind(this));
		this.updateAncestorCheckboxes();
	},
	
	
	
	// Update a checkbox's appearance to match its internal partially-checked state.
	// *Note: does not actually examine descendant checkboxes!
	//
	updateCheckbox : function(checkbox) {
		checkbox.setOpacity(checkbox.partiallyChecked ? 0.5 : 1);
	},
	
	
	
	// Set all of a checkbox's descendants to some state.
	//
	checkDescendantCheckboxesInContainers : function(checkbox, childCheckboxContainerBehaviour) {
		checkbox.partiallyChecked = false;
		this.updateCheckbox(checkbox);
		
		// Set all at this level.
		childCheckboxContainerBehaviour.childCheckboxes.each((function(childCheckbox) {
			childCheckbox.checked = checkbox.checked;
			childCheckbox.partiallyChecked = false;
			this.updateCheckbox(childCheckbox);
		}).bind(this));
		
		// Branch out
		childCheckboxContainerBehaviour.childBehaviours.each((function(childBehaviour) {
			childBehaviour.checkDescendantCheckboxesInContainers(checkbox, childBehaviour);
		}).bind(this));
	},
	
	
	
	// Ensures all ancestor checkboxes of this checkbox-container are accurate.
	// This *does* update the appearance, e.g. to reflect the partially-checked state.
	//
	updateAncestorCheckboxes : function() {
		// No more parentCheckboxes? Work is done.
		if (!this.parentCheckbox) return;
		
		// Get aggregate checked state
		var allChecked = true;
		var noneChecked = true;
		
		this.childCheckboxes.each((function(childCheckbox) {
			allChecked &= childCheckbox.checked & !childCheckbox.partiallyChecked;
			noneChecked &= !childCheckbox.checked & !childCheckbox.partiallyChecked;
		}).bind(this));
	
		this.parentCheckbox.checked = !noneChecked;
		this.parentCheckbox.partiallyChecked = (!allChecked && !noneChecked);
		this.updateCheckbox(this.parentCheckbox);
		
		// Otherwise, ensure next parent checkbox is accurate too.
		this.parentCheckboxContainerBehaviour.updateAncestorCheckboxes();
	}
});
