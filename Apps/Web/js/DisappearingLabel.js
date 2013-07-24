function DisappearingLabel(id, label)
{
	this.obj = document.getElementById(id);
	this.label = label;
	this.canShow = true;

	this.obj.value = label;
	
	// Include Prototype.js for the functions below.
	Event.observe(this.obj, 'focus', hide.bindAsEventListener(this));
	Event.observe(this.obj, 'blur', show.bindAsEventListener(this));
}

function show()
{
	if (this.canShow)
	{
		if (this.obj.value == '' )
		{
			this.obj.value = this.label;
		}
		else if (this.obj.value != this.label)
		{
			this.canShow = false;
		}
	}
}

function hide()
{
	if (this.obj.value == this.label)
	{
		this.obj.value = '';	
	}
}
