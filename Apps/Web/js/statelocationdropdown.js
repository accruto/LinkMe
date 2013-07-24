<!-- salary drop down lists -->
var stateOptions = new Array();
var saOptions = new Array(
	new Option('',''),
	new Option('Adelaide', 'Adelaide'),
	new Option('Port Adelaide', 'Port Adelaide'),
	new Option('Victor Harbour', 'Victor Harbour')
);
stateOptions['SA'] = saOptions;

var vicOptions = new Array(
	new Option('',''),
	new Option('Melbourne', 'Melbourne'),
	new Option('Geelong', 'Geelong'),
	new Option('Ballarat', 'Ballarat'),
	new Option('Bendigo', 'Bendigo')
);
stateOptions['VIC'] = vicOptions;

var nswOptions = new Array(
	new Option('',''),
	new Option('Sydney', 'Sydney'),
	new Option('Newcastle', 'Newcastle'),
	new Option('Wagga Wagga', 'Wagga Wagga')
);
stateOptions['NSW'] = nswOptions;

var qldOptions = new Array(
	new Option('',''),
	new Option('Brisbane', 'Brisbane'),
	new Option('Gold Coast', 'Gold Coast'),
	new Option('Cairns', 'Cairns')
);
stateOptions['QLD'] = qldOptions;

var waOptions = new Array(
	new Option('',''),
	new Option('Perth', 'Perth'),
	new Option('Fremantle', 'Fremantle')
);
stateOptions['WA'] = waOptions;


var tasOptions = new Array(
	new Option('',''),
	new Option('Hobart', 'Hobart'),
	new Option('Launceston', 'Launceston')
);
stateOptions['TAS'] = tasOptions;

var ntOptions = new Array(
	new Option('',''),
	new Option('Darwin', 'Darwin'),
	new Option('Alice Springs', 'Alice Springs')
);
stateOptions['NT'] = ntOptions;

var actOptions = new Array(
	new Option('',''),
	new Option('Canberra', 'Canberra')
);

stateOptions['ACT'] = actOptions;

function changeOptions(source, targetId)
{
	loadOptions(targetId,source.options[source.selectedIndex].value,'');
}

function loadOptions(targetId, listId, selectedValue)
{
	var target = document.getElementById(targetId);
	target.options.length = 0;
	
	if(listId == '')
	  listId = 'NSW';
	  
	var options = stateOptions[listId];
	
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