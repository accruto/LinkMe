/* == Javascript for the Overlay Popup ==
 * 
 * displayOverlayPopup() puts a translucent black layer 
 * over the current display, and puts a message or 
 * dialog box on top.
 * 
 * banishOverlayPopup() removes the popup, allowing the
 * user to access the original screen.
 */
 
var CONST_PIXELS = 'px';

var OverlayPopup = Class.create({
    initialize : function() {
        this.initialised = false;
    },
    
    populate : function(contentHTML, classesToFunctionsHash) 
    {   
        if(this.initialised == false) {
            var hostingParent = null;
            if($$('form').length == 0)
                hostingParent = document.body;
            else
                hostingParent = $$('form')[0];
            
            this.background = new Element('div', {'id': 'overlay-background'});
            
            hostingParent.appendChild(this.background);
            this.background.hide();

            this.container = new Element('div', {'id': 'overlay-popup-container'});
            hostingParent.appendChild(this.container);
            this.container.hide();

            this.contentDiv = new Element('div', {'id': 'overlay-popup-content'});
            this.container.appendChild(this.contentDiv);
            
            this.messageDiv = new Element('div', {'id': 'overlay-popup-message'});
            this.contentDiv.appendChild(this.messageDiv);

            this.buttonDiv = new Element('div', {'id': 'overlay-popup-buttons'});
            this.contentDiv.appendChild(this.buttonDiv);
            
            this.initialised = true;
        }
            
        //This is to support native DOM objects
        if(contentHTML.parentNode) {                    
            this.tempNode = contentHTML;
            this.tempParentNode = contentHTML.parentNode;                        
            this.tempNode.parentNode.removeChild(this.tempNode);            
            this.messageDiv.appendChild(this.tempNode);
        } else {
            this.messageDiv.update(contentHTML);
            this.tempNode = null;
            this.tempParentNode = null;
        }            
        
        this.buttonDiv.update('');
        classesToFunctionsHash.each(function(pair) {
            var button = new Element('input', {'type': 'button'});
            button.addClassName(pair.key);
            this.buttonDiv.insert(button);
            button.observe('click', pair.value.bindAsEventListener(this));
        }, this);
    },
    
    populateWithHtml : function(contentHTML)
    {
        this.populate(contentHTML, $H({}));
    },
    
    serverPopulationCallback : function(request)
    {
        this.populateWithHtml(request.responseText);
        this.display();
    },

    populateFromServer : function(url, pars)
    {
        this.request = new Ajax.Request( url, {method: 'get', parameters: pars, onComplete: this.serverPopulationCallback.bindAsEventListener(this)});
    },
    
    display : function() 
    {
        //Recalculate bacground size every time
        this.background.setStyle({
            'height': document.body.scrollHeight + CONST_PIXELS,
            'width': document.body.scrollWidth + CONST_PIXELS
        });
            
        this.background.show();
        this.container.show();
        
        LinkMeUI.Utils.ScrollTracker.StartTrack(this.container);
    },

    banish : function() 
    {
        if(this.tempNode != null && this.tempParentNode != null) {
            this.tempNode.parentNode.removeChild(this.tempNode);
            this.tempParentNode.appendChild(this.tempNode);
            
            this.tempNode = null;
            this.tempParentNode = null;
        }
        
        LinkMeUI.Utils.ScrollTracker.StopTrack(this.container);
        
        this.container.hide();
        this.background.hide();
    }
});

/* == Legacy functions == */

function displayOverlayPopup(contentHTML)
{
    overlayPopup.populateWithHtml(contentHTML);
    overlayPopup.display();
}

function banishOverlayPopup()
{
    overlayPopup.banish();
}

function populateOverlayPopup(url, pars)
{
    overlayPopup.populateFromServer(url, pars);
}
/* == End Overlay Popup == */

//var overlayPopup;
//Event.observe(window, 'load', function() {
    window.overlayPopup = new OverlayPopup();
//});
