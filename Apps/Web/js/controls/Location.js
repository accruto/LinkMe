
function getLocationParameter(autocompleter, name)
{
    var parametersArray = autocompleter.options.defaultParams.split('&');
    for (i = 0; i < parametersArray.length; i++)
    {
        var parameter = parametersArray[i].split('=');
        if (parameter[0] == name)
            return decodeURIComponent(parameter[1]);
    }
    
    return '';
}

function setLocationParameter(autocompleter, name, value)
{
    var found = false;
    var parametersArray = autocompleter.options.defaultParams.split('&');
    for (i = 0; i < parametersArray.length; i++)
    {
        var parameter = parametersArray[i].split('=');
        if (parameter[0] == name)
        {
            parametersArray[i] = name + '=' + encodeURIComponent(value);
            found = true;
            break;
        }
    }

    parameters = '';
    if (parametersArray.length > 0)
    {
        parameters = parametersArray[0];
        for (i = 1; i < parametersArray.length; i++)
            parameters += '&' + parametersArray[i];
    }
    
    if (!found)
        parameters += '&' + name + '=' + encodeURIComponent(value);
    
    autocompleter.options.defaultParams = parameters;
}

function resolveLocation(locationElement, confirmationElement, autocompleter, method)
{
    if (locationElement != null)
    {
        // Store the unresolved value.
        
        var unresolved = locationElement.value;
        if (unresolved != "")
        {
            // Invoke the method.
        
            var country = getLocationParameter(autocompleter, 'country');
            var resolved;
            if (method == "NamedLocation")
                resolved = LinkMe.Web.UI.Controls.Common.ResolveLocation.ResolveNamedLocation(country, unresolved);
            else
                resolved = LinkMe.Web.UI.Controls.Common.ResolveLocation.ResolvePostalSuburb(country, unresolved);
            
            if (resolved.value != "")
            {
                if (resolved.value != unresolved)
                {
                    // Resolved.
                    
                    locationElement.value = resolved.value;
                    setLocationParameter(autocompleter, 'unresolved', unresolved);
                    toggleConfirmationDisplay('true');
                }
                else
                {
                    toggleConfirmationDisplay('false');
                }
            }
            else if (resolved.value == "")
            {
                // Unresolved.
                
                toggleConfirmationDisplay('true');                
            }
        }
    }
}

function rejectLocation(locationElement, confirmationElement, autocompleter)
{
    if (locationElement != null)
    {
        // Get the unresolved value.
        toggleConfirmationDisplay('false');
    
        var unresolved = getLocationParameter(autocompleter, 'unresolved');
        locationElement.value = unresolved;
        locationElement.select();
        locationElement.focus();
    }
}
function showLocationConfirmationMessage(confirmationElement, message, confirm)
{
    setLocationConfirmationMessage(confirmationElement, message, confirm);
    updateLocationConfirmationDisplay(confirmationElement, "block");
}

function updateLocationConfirmationDisplay(confirmationElement, value)
{
    if (confirmationElement != null)
        confirmationElement.style.display = value;
}

