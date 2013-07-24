


function changeOptions(source, targetId, defaultValue, sourceOptions)
{
	loadOptions(targetId,source.options[source.selectedIndex].value,'',defaultValue, sourceOptions);
}

function loadOptions(targetId, listId, selectedValue, defaultValue, sourceOptions)
{

	var target = document.getElementById(targetId);
	target.options.length = 0;
	
	if(listId == '')
	  listId = defaultValue;
	  
	// Hack: currently hyphens are allowed in entity values.
	//       however, javascript forbids hyphens in variable names. so....
	listId = listId.replace('-','');
	
	var options = sourceOptions[listId.toLowerCase()];


	for(var idx=0; idx < options.length; idx++)
	{
		var newOption = new Option(options[idx].text, options[idx].value) ;
		
		target.options[idx] = newOption;
		
		if(selectedValue == '' && idx==0)
		{
			target.options[idx].selected = true;
		}
		else if(newOption.value == selectedValue)
		{
			target.options[idx].selected = true;
		}
		else
		{
			target.options[idx].selected = false;
		}
	}
}