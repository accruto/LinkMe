(function($) {

    setDistanceSliderValue = function() {
    }

    setDefaultDistanceSliderValue = function() {
    }

    initializeDistance = function(distance, distances, defaultDistance) {

        setDistanceSliderLabel = function(value) {
            $("#dist-range").parent("div").parent("div").find("div.range:eq(0)").html((value == distances.length - 1 ? '' : 'within ') + distances[value] + ((value == distances.length - 1) ? '+' : '') + ' km');
        }

        setDistanceSliderValue = function(newDistance) {

            var value = distances.length / 2;
            for (var index = 0; index < distances.length; ++index) {
                if (distances[index] == newDistance) {
                    value = index;
                    break;
                }
            }

            setDistanceSliderLabel(value);
            $("#dist-range").slider("option", "value", value);
        }

        setDefaultDistanceSliderValue = function() {
            setDistanceSliderValue(defaultDistance);
        }

        var range = $("#dist-range");
        range.slider({
            range: "min",
            value: 3,
            min: 0,
            max: distances.length - 1,
            step: 1,
            slide: function(event, ui) {
                setDistanceSliderLabel(ui.value);
                $("#Distance option:nth-child(" + (ui.value + 1) + ")").attr("selected", "selected");
            },
            stop: function(event, ui) {
                activateSection("distance_section");
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            }
        });

        range.parent("div").parent("div").find("div.minrange:eq(0)").html(distances[0] + ' km');
        range.parent("div").parent("div").find("div.maxrange:eq(0)").html(distances[distances.length - 1] + '+ km');

        if (distance != null)
            setDistanceSliderValue(distance);
        else
            setDistanceSliderValue(defaultDistance);
    }

    setRecencySliderValue = function() {
    }

    setDefaultRecencySliderValue = function() {
    }

    initializeRecency = function(recency, recencies, defaultRecency) {

        setRecencySliderLabel = function(value) {
            $("#time-range").parent("div").parent("div").find("div.range:eq(0)").html(recencies[value].label);
        }

        setRecencySliderValue = function(newRecency) {

            var value = recencies.length / 2;
            for (var index = 0; index < recencies.length; ++index) {
                if (recencies[index].days == newRecency) {
                    value = index;
                    break;
                }
            }

            setRecencySliderLabel(value);
            $("#time-range").slider("option", "value", value);
        }

        setDefaultRecencySliderValue = function() {
            setRecencySliderValue(defaultRecency);
        }

        var range = $("#time-range");
        range.slider({
            range: true,
            range: "min",
            value: 5,
            min: 0,
            max: recencies.length - 1,
            step: 1,
            slide: function(event, ui) {
                setRecencySliderLabel(ui.value);
                $("#Recency").val(recencies[ui.value].days);
            },
            stop: function(event, ui) {
                activateSection("resume-recency_section");
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            }
        });

        range.parent("div").parent("div").find("div.minrange:eq(0)").html(recencies[0].label);
        range.parent("div").parent("div").find("div.maxrange:eq(0)").html(recencies[recencies.length - 1].label);

        setRecencySliderValue(recency);
    }

    setSalarySliderValues = function() {
    }

    setDefaultSalarySliderValues = function() {
    }

    initializeSalary = function(lowerBound, upperBound, minSalary, maxSalary, stepSalary) {

        setSalarySliderLabel = function(lowerBound, upperBound) {

            var label = "";
            if (lowerBound == minSalary && upperBound == maxSalary) {
                /*label = "$" + toFormattedDigits(maxSalary) + "+";*/
                label = "Any salary";
            }
            else if (lowerBound == minSalary) {
                label = "up to $" + toFormattedDigits(upperBound);
            }
            else if (upperBound == maxSalary) {
                label = "$" + toFormattedDigits(lowerBound) + "+";
            }
            else {
                label = "$" + toFormattedDigits(lowerBound) + " - $" + toFormattedDigits(upperBound);
            }

            $("#salary-range").parent("div").parent("div").find("div.range:eq(0)").html(label);
        }

        setSalarySliderValues = function(lowerBound, upperBound) {
            if (lowerBound == null)
                lowerBound = minSalary;
            if (upperBound == null)
                upperBound = maxSalary;
            setSalarySliderLabel(lowerBound, upperBound);
            $("#salary-range").slider("option", "values", [lowerBound, upperBound]);
        }

        setDefaultSalarySliderValues = function() {
            setSalarySliderValues(null, null);
        }

        $("#salary-range").slider({
            range: true,
            min: minSalary,
            max: maxSalary,
            step: stepSalary,
            values: [minSalary, maxSalary],
            slide: function(event, ui) {
                setSalarySliderLabel(ui.values[0], ui.values[1]);
                $("#SalaryLowerBound").val(ui.values[0]);
                $("#SalaryUpperBound").val(ui.values[1]);
            },
            stop: function(event, ui) {
                activateSection("desired-salary_section");
                $('html, body').animate({ scrollTop: 0 }, 'slow');
                updateResults(false);
            }
        });

        $("#salary-range").parent("div").parent("div").find("div.minrange:eq(0)").html('$' + toFormattedDigits(minSalary));
        $("#salary-range").parent("div").parent("div").find("div.maxrange:eq(0)").html('$' + toFormattedDigits(maxSalary) + '+');

        setSalarySliderValues(lowerBound, upperBound);
    }

    $(document).ready(function() {
        $(".inputs_for_sliders").hide();
    });
})(jQuery);