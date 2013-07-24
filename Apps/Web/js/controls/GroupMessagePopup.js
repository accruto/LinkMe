GroupSendMessageHandler = function(linkId, txtSubjId, txtMessageId) {
    this.linkId = linkId;
    this.txtSubjId = txtSubjId;
    this.txtMessageId = txtMessageId;
    
    this.send = function() {        
        var sV = Validation.validate($(this.txtSubjId));
        var tV = Validation.validate($(this.txtMessageId));
        if(!sV || !tV)
            return;
        document.location = $(this.linkId).href;
    }
}      

ShowGroupMessageSendPopup = function(divId, linkId, txtSubjId, txtMessageId) {
    var handler = new GroupSendMessageHandler(linkId, txtSubjId, txtMessageId);
    var classesToFunctionsHash = $H({'send-button' : handler.send.bind(handler), 'cancel-button' : overlayPopup.banish});
    overlayPopup.populate($(divId), classesToFunctionsHash);
    overlayPopup.display();
}

