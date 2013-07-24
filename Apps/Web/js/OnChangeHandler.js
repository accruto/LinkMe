/*
Contains the hacks necessary to prevent IE from annoying the hell out of the user with the
dropdown list onchange event: without these hacks it fires onchange whenever the user scrolls
through the list with either the keyboard or the mouse (when they usually intend to scroll the
page instead) and the onchange action is taken immediately, rather than when they click or
press Enter or the control loses focus. This makes it behave more like a normal browser.
*/

function OnChangeHandler()
{
    this.ignoreNextOnChange = false;

    this.Observe = function(control, handler)
    {
        Event.observe(control, 'mousewheel', function()
            {
                this.ignoreNextOnChange = true; // Scrolling with mouse - ignore.
            });

        Event.observe(control, 'click', function()
            {
                this.ignoreNextOnChange = false; // Clicked - process changes.
            });
    
        Event.observe(control, 'keydown', function(e)
            {
                if (e.keyCode == 38 || e.keyCode == 40)
                    this.ignoreNextOnChange = true; // Scrolling with keyboard - ignore.
            });

        Event.observe(control, 'keypress', function(e)
            {
                if (e.keyCode == 13)
                    handler(e); // Pressed enter - raise event.
            });

        Event.observe(control, 'change', function(e)
            {
                if (!this.ignoreNextOnChange)
                    handler(e);
            });
    };
}
