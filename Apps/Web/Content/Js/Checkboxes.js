(function ($) {

    var _update = function(checkbox, classes) {
        var hover = checkbox.data("hover");
        var down = checkbox.data("down");
        var checked = checkbox.data("checked");
        checkbox.toggleClass(classes.hover, hover && !down && !checked);
        checkbox.toggleClass(classes.down, down && !checked);
        checkbox.toggleClass(classes.checked, !hover && !down && checked);
        checkbox.toggleClass(classes.checkedHover, hover && !down && checked);
        checkbox.toggleClass(classes.checkedDown, down && checked);
    };

    var _radioButtons = function (radioButtons, checked, classes, onChange) {

        radioButtons.each(function () {

            // Initial state.
            
            var $this = $(this);
            $this.data("hover", false);
            $this.data("down", false);
            $this.data("checked", false);
            _update($this, classes);

            $this.mouseenter(function () {
                var $this = $(this);
                $this.data("hover", true);
                _update($this, classes);
                return false;
            });
            
            $this.mouseleave(function() {
                var $this = $(this);
                $this.data("hover", false);
                $this.data("down", false);
                _update($this, classes);
                return false;
            });

            $this.mousedown(function() {
                var $this = $(this);
                $this.data("down", true);
                _update($this, classes);
                return false;
            });

            $this.mouseup(function() {

                // Clear all first.
                
                radioButtons.each(function() {
                    var $this = $(this);
                    $this.data("checked", false);
                    _update($this, classes);
                });
                
                // Make sure this one is checked.

                var $this = $(this);
                $this.data("checked", true);
                $this.data("down", false);
                _update($this, classes);
                
                if (onChange) {
                    onChange($this);
                }

                return false;
            });
            
        });
        
        // If there is an initial one checked check it now.

        if (checked) {
            var $checked = $(checked);
            if ($checked.length == 1) {
                $checked.data("checked", true);
                _update($checked, classes);

                if (onChange) {
                    onChange($checked);
                }
            }
        }
    };

    $.fn.largeRadioButtons = function (checked, onChange) {
        _radioButtons(
            this,
            checked,
            {
                hover: "large-checkbox-hover",
                down: "large-checkbox-down",
                checked: "large-checkbox-checked",
                checkedHover: "large-checkbox-checked-hover",
                checkedDown: "large-checkbox-checked-down"
            },
            onChange);
    };

})(jQuery);
