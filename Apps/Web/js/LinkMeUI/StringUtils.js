LinkMeUI.StringUtils = {
    uniqueToken : 'AD66FF81-6454-4459-B0EC-E4B17045890D',
    
    ReplaceLineBreaksWithToken : function(text) {
        return text.replace(/\r/g, '').replace(/\n/g, this.uniqueToken);
    },
    
    ReplaceTokenWithLineBreaks : function(text) {
        var re = new RegExp(this.uniqueToken, 'g');
        return text.replace(re, '\n');
    }, 
        
    HtmlToText :function(html) {
        html = html.replace(new RegExp('<br.*?>', 'gi'), '\n');
        var wT = this.ReplaceLineBreaksWithToken(html);
        var unescaped = wT.unescapeHTML();
        var res = this.ReplaceTokenWithLineBreaks(unescaped);
        return res;
    },   

    TextToHtml : function (text) {
        var wT = this.ReplaceLineBreaksWithToken(text);
        var escaped = wT.escapeHTML();
        var withoutT = this.ReplaceTokenWithLineBreaks(escaped);
	    return withoutT.replace(new RegExp('\n', 'g'), '<br />');	    	    
    },
    
    // Sets the query string parameter value in an existing URL, replacing any existing parameter with that name.
    SetQueryStringParameter : function(queryString, paramName, paramValue) {
        var pairs = queryString.toQueryParams();
        pairs[paramName] = paramValue;
        return Object.toQueryString(pairs);
    }
}
