var SuccessHighlightColour = '#50f550';
var FailureHighlightColour = '#f55050';

var currentItemIndex;
var currentEmailLinkId;
var currentActionConfirmation; // Used in the confirmation message.
var targetListId, sourceListId; // Used to update the sidebar folder list.
var editingNewNote = false;
var editedIndex;

function ToggleResumeOptions(strControlName)
{
    var element = $(strControlName);

    var elExpando = element.select(".js_expando");
    var elContracto = element.select(".js_contracto");
    var elRevealMe = element.select(".js_reveal-me");

    elExpando.each  (function(el){el.toggle()});
    elContracto.each(function(el){el.toggle()});
    elRevealMe.each (function(el){el.toggle()});
}

function OnResumeEmailed(originalRequest)
{
	var spanLink = $(currentEmailLinkId);
	
	var response = originalRequest.responseText;
	var message = 'An error occurred';
	var success = false;

	if (response == 'Success')
	{
	    message = 'Resume emailed to you';
	    success = true;
	}
	else if (response == 'NoCredits')
	{
	    message = 'You have no contact credits';
	}
	else if (response == 'NotLoggedIn')
	{
	    message = 'You have been logged out';
	}
	else if (response == 'ResumeUnavailable')
	{
	    message = 'This resume is not available';
	}
	else if (originalRequest.status != 0)
	{
	    message = 'An error occurred';
	}
	
	spanLink.update(message);
	
	if (success)
	{
	    HighlightElement(spanLink, SuccessHighlightColour);
	}
	else
	{
	    HighlightElement(spanLink, FailureHighlightColour);
	}
}

function AddOrMoveCandidate(ddlAddTo, candidateId, index, isJobAd, confirmationVerb, removeFromListId)
{
    var selectedValue = ddlAddTo[ddlAddTo.selectedIndex].value;

    // First check for some placeholder values that simply redirect the user.
    
    if (selectedValue == 'RequireLogin')
    {
        window.location.pathname = LinkMeUI.ApplicationPath + '/employers/guests/LoginRequired.aspx?actionText='
            + (isJobAd ? 'manage+jobs' : 'save+candidates');
        return;
    }
    else if (selectedValue == 'RequirePurchase')
    {
        window.location.pathname = LinkMeUI.ApplicationPath + '/employers/products/neworder';
        return;
    }

    currentItemIndex = index;
    targetListId = selectedValue;
    sourceListId = removeFromListId;
    currentActionConfirmation = 'Candidate ' + confirmationVerb;
    var onCompleteFunction = (removeFromListId ? processRemoveResponse : showAjaxServiceResponse);

	if (selectedValue.indexOf('(none)') != 0)
	{
		// show loading...
		$('Loading' + index).src = LinkMeUI.ApplicationPath + '/ui/images/universal/loading.gif';
		$('Loading' + index).show();
		$('UpdatingText' + index).innerHTML = 'In progress...';

		var action = (removeFromListId ? 'MoveCandidate' : 'AddCandidate');
		var idParam = (isJobAd ? 'jobAdId' : 'listId');

		var url = LinkMeUI.ApplicationPath + '/service/CandidateListService.ashx';
		var pars = 'action=' + action +
				'&candidateId=' + candidateId +
				'&' + idParam + '=' + selectedValue; 
        if (removeFromListId)
        {
            pars += '&fromListId=' + removeFromListId;
        }
		
		var myAjax = new Ajax.Request( url, { method: 'post', parameters: pars, onComplete: onCompleteFunction });
    }
}

function RemoveCandidate(candidateId, listId, index)
{
    var action = 'RemoveCandidate';
	var url = LinkMeUI.ApplicationPath + '/service/CandidateListService.ashx';
	var pars = 'action=' + action +
	           '&candidateId=' + candidateId +
	           '&listId=' + listId;

	currentItemIndex = index;
	currentActionConfirmation = 'Candidate removed';
	targetListId = listId;

    var myAjax = new Ajax.Request( url, { method: 'post', parameters: pars, onComplete: processRemoveResponse});
}

function processRemoveResponse(originalRequest)
{
    if (showAjaxServiceResponse(originalRequest))
    {
        var currentItem = $('removableItem_'+ currentItemIndex);
        new Effect.Fade(currentItem);
    }
}

function showAjaxServiceResponse(originalRequest)
{	
    var response = originalRequest.responseText;
    var space = response.indexOf(' ');
    var resultCode = (space == -1 ? response : response.substring(0, space));

	var success;

	if (resultCode == 'Success' || resultCode == 'AlreadyExists')
	{
	    // Confirmation text below the action
		$('UpdatingText' + currentItemIndex).innerHTML =
		    (resultCode == 'Success' ? currentActionConfirmation : 'Already in that folder');
		HighlightElement('UpdatingText' + currentItemIndex, SuccessHighlightColour);
		
		if (space == -1)
		    throw 'Extra data was expected for a successfull CandidateListService call.';
		
		success = true;
	}
	else if (originalRequest.status != 0)
	{
	    var message = (resultCode == 'NotLoggedIn' ? 'You have been logged out' : 'An error occurred');
		$('UpdatingText' + currentItemIndex).innerHTML = message;
		HighlightElement('UpdatingText' + currentItemIndex, FailureHighlightColour);
		success = false;
	}
	
	$('Loading' + currentItemIndex).src = LinkMeUI.ApplicationPath + '/ui/img/transparent.gif';
	$('Loading' + currentItemIndex).hide();
	
	return success;
}

function UpdateFoldersSideBarCallback(request)
{
    var divFoldersSidebar = $('divFoldersSidebar');
    if (!divFoldersSidebar)
        throw 'divFoldersSidebar not found - UpdateFoldersSideBarCallback should not have been called.';

    divFoldersSidebar.parentNode.innerHTML = request.responseText;
}

function AddNewListToAllDropdowns(listName, listId)
{
    var lists = $$('select.js_candidate_lists_list');
    lists.each(function(list)
    {
        // Create a new list item for each list - cloneNode() would be nice, but it seems to be stuffed in IE7
        var newListItem = document.createElement('option');
        newListItem.text = listName;
        newListItem.value = listId;

        var beforeOption = null;
        var newIndex = list.length;
        
        for (var i = list.length - 1; i > 0; i--)
        {
            var item = list.options[i];
            if (item.value.startsWith('(none)') || newListItem.text.toUpperCase() > item.text.toUpperCase())
                break;

            if (item.value == 'default')
            {
                // Reached the default list, so add a separator before adding the actual new item.

                var separator = document.createElement('option');
                separator.text = '---------------------';
                separator.value = '(none) - sep 2';
                list.add(separator, null);

                break;
            }

            beforeOption = list.options[i]; // For normal browsers
            newIndex = i; // For IE
        }

        try
        {
            list.add(newListItem, beforeOption); // For normal browsers
        }
        catch (ex)
        {
            list.add(newListItem, newIndex); // For IE
        }
    });
}

function HighlightElement(elementId, startColour)
{
	new Effect.Highlight(elementId, { duration: 1.5, startcolor:startColour, endcolor:'#ffffff'});
}
