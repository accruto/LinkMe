function MessageLostFocus() 
{
    if (this.value == '')
    {
        //this.removeClassName('input');
        this.addClassName('empty');
        this.value = BlankWhiteboardText;
    }
}

function MessageGotFocus()
{
    if (this.hasClassName('empty'))
    {
        this.removeClassName('empty');
        //this.addClassName('input');
        this.value = '';
    }
}