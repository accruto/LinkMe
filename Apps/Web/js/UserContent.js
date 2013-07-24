/*
 * UserContent.js
 * 
 * This file is for Javascript code pertaining to
 * UserContentItems and UserContentRemovalRequests
 *
 */

var UserContent = Class.create({
    initialize : function() {
        this.requestPopup = new OverlayPopup();
    },

    ShowRemovalRequestPopup : function(itemId) {
        var html = '<h1>Report inappropriate content</h1>' +
                   '<p>LinkMe treats all abuses of its service seriously. We will only respond to you if we need more information.</p>' +
                   '<p>Please provide an explanation of why this content is inappropriate (required)</p>' +
                   '<textarea id="txtRequestReason" style="width: 95%" />';

        this.requestPopup.populate(html, 
                                   $H({ 'send-button': this.GetSendRemovalRequestFunction(itemId).bindAsEventListener(this), 
                                     'cancel-button': this.requestPopup.banish }));
        
        this.requestPopup.display();
        $('txtRequestReason').focus();
    },
    
    GetSendRemovalRequestFunction : function(itemId) {
        var url = LinkMeUI.ApplicationPath + '/service/PostUserContentRemovalRequest.ashx';

        return function() {
            var box = $('txtRequestReason');
            var reason = $F(box);
            if (reason.length == 0)
            {
                Element.insert(box, {after: '<div style="color:red;">Please explain why this content is inappropriate.</div>'});
            }
            else
            {
                var pars = { 'itemId' : itemId,
                             'url' : window.location,
                             'reason' : reason };
                var request = new Ajax.Request( url, { parameters: pars, onComplete: this.SendRemovalRequestCallback.bindAsEventListener(this)});
            }
        };
    },
    
    SendRemovalRequestCallback : function(response) {
        var secNav = null;
        if($('secondary-nav') == null) { secNav = document.body; }
        else { secNav = $('secondary-nav'); }

        var id = 'error-message';
        var msg = 'There was an error sending your report. Please refresh the page and try again.';

        if (response.responseText == 'Success')
        {
            id = 'confirm-message';
            msg = 'Thanks. Your report will be investigated.';
        }
        
        if ($(id) != null) { $(id).hide(); }
        
        var conf = new Element('div', {'id': id});
        conf.innerHTML = '<ul><li>' + msg + '</li></ul>';
        Element.insert(secNav, {after: conf});

        this.requestPopup.banish();
        window.scroll(0,0);
    }
});

//Event.observe(document, 'load', function() {
        window.userContent = new UserContent();
//    });