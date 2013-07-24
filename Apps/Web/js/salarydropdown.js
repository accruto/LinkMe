<!-- salary drop down lists -->
var salaryOptions = new Array();
var yearlyOptions = new Array(
	new Option('',''),
	new Option('less than $30000', 'less than $30000'),
	new Option('$30001 to $45000', '$30001 to $45000'),
	new Option('$45001 to $60000', '$45001 to $60000'),
	new Option('$60001 to $75000', '$60001 to $75000'),
	new Option('$75001 to $90000', '$75001 to $90000'),
	new Option('over $90000', 'over $90000')
);
salaryOptions['Year'] = yearlyOptions;
var weeklyOptions = new Array(
	new Option('',''),
	new Option('less than $500', 'less than $500'),
	new Option('$501 to $750', '$501 to $750'),
	new Option('$751 to $1000', '$751 to $1000'),
	new Option('$1001 to $1250', '$1001 to $1250'),
	new Option('$1251 to $1500', '$1251 to $1500'),
	new Option('$1501 to $1750', '$1501 to $1750'),
	new Option('over $1750', 'over $1750')
);
salaryOptions['Week'] = weeklyOptions;
var hourlyOptions = new Array(
	new Option('',''),
	new Option('less than $20', 'less than $20'),
	new Option('$21 to $30', '$21 to $30'),
	new Option('$31 to $40', '$31 to $40'),
	new Option('$41 to $50', '$41 to $50'),
	new Option('$51 to $60', '$51 to $60'),
	new Option('$61 to $70', '$61 to $70'),
	new Option('$71 to $80', '$71 to $80'),
	new Option('$81 to $90', '$81 to $90'),
	new Option('$91 to $100', '$91 to $100'),
	new Option('$101 to $110', '$101 to $110'),
	new Option('$111 to $120', '$111 to $120'),
	new Option('$121 to $130', '$121 to $130'),
	new Option('$131 to $140', '$131 to $140'),
	new Option('$141 to $150', '$141 to $150'),
	new Option('$151 to $160', '$151 to $160'),
	new Option('over $160', 'over $160')
);
salaryOptions['Hour'] = hourlyOptions;
var monthlyOptions = new Array(
	new Option('',''),
	new Option('less than $2500', 'less than $2500'),
	new Option('$2501 to $3750', '$2501 to $3750'),
	new Option('$3751 to $5000', '$3751 to $5000'),
	new Option('$5001 to $6250', '$5001 to $6250'),
	new Option('$6251 to $7500', '$6251 to $7500'),
	new Option('over $7500', 'over $7500')
);
salaryOptions['Month'] = monthlyOptions;

function changeOptions(source, targetId)
{
	loadOptions(targetId,source.options[source.selectedIndex].value,'');
}
function loadOptions(targetId, listId, selectedValue)
{
	var target = document.getElementById(targetId);
	target.options.length = 0;
	
	if(listId == '')
	  listId = 'Year';
	  
	var options = salaryOptions[listId];
	
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