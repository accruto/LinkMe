var FailureHighlightColour = '#f55050';


function AjaxRequestAndRefresh(method, url, pars, elementIdToRefresh, suffix, successString)
{
    $('AjaxProgress_' + suffix).src = LinkMeUI.ApplicationPath + '/ui/images/universal/loading.gif';
    $('AjaxProgress_' + suffix).title = '';

    var myAjax = new Ajax.Request(url,
    {
        method: method, parameters: pars, onComplete: function (request)
        {
            ProcessResponseAndRefresh(request, elementIdToRefresh, suffix, successString);
        } 
    });
}

function ProcessResponseAndRefresh(request, elementIdToRefresh, suffix, successString)
{
    var response = request.responseText;

    if (response.indexOf(successString) != -1) {
        // Success - update the element to contain the response HTML
        $(elementIdToRefresh).update(response);
    }
    else if (request.status != 0) // Don't show an error if the user simply navigated away the response was received.
    {
        // Treat anything else as failure (it's probably the Server Error page).
        DisplayError(elementIdToRefresh, suffix);
    }
}

function DisplayError(elementId, suffix)
{
    $('AjaxProgress_' + suffix).src = LinkMeUI.ApplicationPath + '/ui/img/error.gif';
    $('AjaxProgress_' + suffix).title = 'An error has occurred on the server';

    var errorElement = $(elementId + suffix + '_error');
    if (!errorElement)
    {
        errorElement = $(elementId + '_error');
    }
    if (errorElement)
    {
        errorElement.show();
    }

    if ($(elementId + suffix))
    {
        HighlightElement(elementId + suffix, FailureHighlightColour);
    }
    else if ($(elementId))
    {
        HighlightElement(elementId, FailureHighlightColour);
    }
}

function HighlightElement(elementId, startColour)
{
    new Effect.Highlight(elementId, { duration: 1.5, startcolor: startColour, endcolor: '#ffffff' });
}

function CancelEvent(event)
{
    event.cancelBubble = true;
    if (event.stopPropagation)
        event.stopPropagation();
    return false;
}
