/* Javascript for Visibility Settings controls */

function RestoreVisibilityBasicSettingsDefaults()
{
    rdoHiVis.checked = false;
    rdoMidVis.checked = true;
    rdoLoVis.checked = false;
    rdoInVis.checked = false;
}

function UncheckAllRadioButtons()
{
    rdoHiVis.checked = false;
    rdoMidVis.checked = false;
    rdoLoVis.checked = false;
    rdoInVis.checked = false;
}

function RestoreVisibilityAdvancedSettingsDefaults()
{
    SetAdvancedCheckboxesToBasicSetting(2);
}

function BasicVisibilityRadioButtonClicked(button)
{
    if (UserHasAdvancedVisibilitySettings)
    {
        // Display OverlayPopup with message and Yes/No buttons
        button_to_change = button;
        displayOverlayPopup(BuildPopupMessage());
    }
    else
    {
        ChangeVisibilitySettings(button);
    }
}

function BuildPopupMessage()
{
     var message = 'Warning - Selecting this option will override the advanced settings that you previously applied.<br />';
     message += 'Do you want to continue?<br /><br />';
     message += '<input type="button" class="yes-button" onClick="banishOverlayPopup();ChangeVisibilitySettings(button_to_change);" /> '
     message += '<input type="button" class="no-button" onClick="banishOverlayPopup();UncheckAllRadioButtons();" /> '
     
     return message;
}

function ChangeVisibilitySettings(button)
{
    button.checked = true;
    
    if (typeof VisibilityRadioButtonStates != 'undefined')
    {
        switch(button)
        {
            case rdoHiVis:
               SetAdvancedCheckboxesToBasicSetting(3);
               break;
            case rdoMidVis:
               SetAdvancedCheckboxesToBasicSetting(2);
               break;
            case rdoLoVis:
               SetAdvancedCheckboxesToBasicSetting(1);
               break;
            case rdoInVis:
               SetAdvancedCheckboxesToBasicSetting(0);
               break;
        }
    }
    
    DisplayVisibilityTextForButton(button);

    
    UserHasAdvancedVisibilitySettings = false;
}

function AdvancedVisibilityCheckboxClick(box)
{
    var boxType = box.parentNode.className;
    var row = $(box.parentNode.parentNode.parentNode);
    
    var temp = row.select('.js_hidden')[0];
    var hidden_box = (temp == null ? null : temp.firstChild);
    
    temp = row.select('.js_first-degree')[0];
    var first_degree_box = (temp == null ? null : temp.firstChild);

    temp = row.select('.js_second-degree')[0];
    var second_degree_box = (temp == null ? null : temp.firstChild);

    temp = row.select('.js_public')[0];
    var public_box = (temp == null ? null : temp.firstChild);
    
    if (row.select('.table-row-name')[0].innerHTML == 'Name')
    {
        if (boxType == 'js_public')
        {
            SetCheckBoxColumnEnabled('js_public', box.checked);
            SetCheckBoxColumnEnabled('js_second-degree', true);
        }
        
        if (boxType == 'js_second-degree')
        {
            SetCheckBoxColumnEnabled('js_public', false);
            SetCheckBoxColumnEnabled('js_second-degree', box.checked);
        }
    }
    
    // checked is the value the box is becoming
    if (boxType == 'js_public' && box.checked)
    {
        CheckTheBox(first_degree_box, true);
        
        CheckTheBox(second_degree_box, true);
    }
    
    if (boxType == 'js_second-degree')
    {
        if (box.checked)
        {
            CheckTheBox(first_degree_box, true);
        }
        else
        {
            CheckTheBox(public_box, false);
        }
    }
    
    if (boxType == 'js_first-degree' && !box.checked)
    {
        CheckTheBox(second_degree_box, false);
            
        CheckTheBox(public_box, false);
    }
    
    if (boxType == 'js_hidden')
    {
        if (box.checked)
        {
            CheckTheBox(first_degree_box, false);
                
            CheckTheBox(second_degree_box, false);
                
            CheckTheBox(public_box, false);
        }
        else
        {
            CheckTheBox(first_degree_box, true);
        }
    }
    else
    {
        CheckTheBox(hidden_box, !AnyCheckBoxIsChecked(new Array(first_degree_box, second_degree_box, public_box)));
    }
    
    switch (BasicSettingMatchingAdvancedSettings())
    {
        case 0:
            ChangeVisibilitySettings(rdoInVis);
            break;
        case 1:
            ChangeVisibilitySettings(rdoLoVis);
            break;
        case 2:
            ChangeVisibilitySettings(rdoMidVis);
            break;
        case 3:
            ChangeVisibilitySettings(rdoHiVis);
            break;
        default:
            rdoInVis.checked = false;
            rdoLoVis.checked = false;
            rdoMidVis.checked = false;
            rdoHiVis.checked = false;
            UserHasAdvancedVisibilitySettings = true;
            break;
    }
}

function CheckTheBox(box, value)
{
    if (box != null)
        box.checked = value;
}

function AnyCheckBoxIsChecked(boxes)
{
    for (var i = 0; i < boxes.length; i++)
    {
        if (boxes[i] != null && boxes[i].checked)
            return true;
    }
    
    return false;
}

function BasicSettingMatchingAdvancedSettings()
{
    var defaultSetting = -1;
    
    var rows = $$('.js_checkbox-row');
    
    for (var setting = 0; setting < VisibilityRadioButtonStates.length; setting++)
    {
        var match = true;
    
        for (var row = 0; row < rows.length; row++)
        {
            var tds = rows[row].getElementsByTagName('td');
            
            // Skip the td holding the display name for the row.
            for (var td = 1; td < tds.length; td++)
            {
                var box = tds[td].getElementsByTagName('input')[0];
            
                // Subtract one from td to match up with VisibilityRadioButtonStates[0]
                if (box != null && box.checked != VisibilityRadioButtonStates[setting][row][td - 1])
                {
                    match = false;
                    break;
                }
            }
            
            if (!match)
                break;
        }
        
        if (match)
        {
            return setting;
        }
    }
    
    return defaultSetting;
}

function SetAdvancedCheckboxesToBasicSetting(settingNumber)
{
    var rows = $$('.js_checkbox-row');
    
    for (var row = 0; row < rows.length; row++)
    {
        var tds = rows[row].getElementsByTagName('td');
        
        // Skip the td holding the display name for the row.
        for (var td = 1; td < tds.length; td++)
        {
            var box = tds[td].getElementsByTagName('input')[0];
        
            // Subtract one from td to match up with VisibilityRadioButtonStates[0]
            if (box != null)
            {
                box.checked = VisibilityRadioButtonStates[settingNumber][row][td - 1];
                
                if (row == 0)
                {
                    if (td == 3)
                    {
                        SetCheckBoxColumnEnabled('js_second-degree', box.checked);
                    }
                    
                    if (td == 4)
                    {
                        SetCheckBoxColumnEnabled('js_public', box.checked);
                    }
                }
            }
        }
    }
}

function DisplayVisibilityTextForButton(button)
{
    switch (button)
    {
        case rdoHiVis:
            elementId = 'rdoHighlyVisible_div';
            break;
        case rdoMidVis:
            elementId = 'rdoModeratelyVisible_div';
            break;
        case rdoLoVis:
            elementId = 'rdoLessVisible_div';
            break;
        case rdoInVis:
            elementId = 'rdoInvisible_div';
            break;
        default:
            elementId = 'custom-visibility-text';
            break;
    }

    var divs = $$('.visibility-explanation-text');
    
    for (var i = 0; i < divs.length; i++)
    {
        divs[i].style['display'] = 'none';
    }

    DisplayElement(elementId, 'block');
}

function DisplayElement(elementId, displayMode)
{
    var el = document.getElementById(elementId);
    
    el.style['display'] = displayMode;
}

