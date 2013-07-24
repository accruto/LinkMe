function SetAllCheckboxes(contactCheckboxIds)
{
    var toggleAllCheckBox = document.getElementById('chkToggleAll');
    var checked = toggleAllCheckBox.checked;
    
    for (var index = 0; index < contactCheckboxIds.length; index++)
    {
        var contactCheckbox = document.getElementById(contactCheckboxIds[index]);
        contactCheckbox.checked = checked;
    }
}
