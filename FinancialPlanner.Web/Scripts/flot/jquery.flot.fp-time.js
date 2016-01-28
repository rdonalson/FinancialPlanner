/* ======================================================================================
 * Pretty handling of time axes.
 * Copyright (c) 2007-2014 IOLA and Ole Laursen.
 * Licensed under the MIT license.
 * Set axis.mode to "time" to enable. See the section "Time series data" in
 * API.txt for details.
 * --------------------------------------------------------------------------
 * Custom Version for use in the Financial Planner based on the original listed above
 * Copyright (c) 2016 Rick Donalson.
 * Licensed under the MIT license.
 * ======================================================================================*/
(function($) {

    var options = {
        xaxis: {
            weekDayInit: 0  // first weekday that you want the weekly ticks to begin on, if not the min day
        }
    };

    /* ---------------------------------------------------------------------------------------
     * Return Browser Time
     * -------------------------------------------------------------------------------------*/
    function dateGenerator(ts) {
        return new Date(ts);
    }
    /* ---------------------------------------------------------------------------------------
     * Time Units in milliseconds
     * -------------------------------------------------------------------------------------*/
    var timeUnitSize = {
        "day": 24 * 60 * 60 * 1000,
        "week": 7 * 24 * 60 * 60 * 1000,
        "month": 30 * 24 * 60 * 60 * 1000,
        "year": 365.2425 * 24 * 60 * 60 * 1000
    };
    /* ---------------------------------------------------------------------------------------
     * Returns a string with the date d formatted according to fmt.
     * A subset of the Open Group's strftime format is supported.
     * -------------------------------------------------------------------------------------*/
    function formatDate(d, fmt, monthNames, dayNames) {

        if (typeof d.strftime == "function") {
            return d.strftime(fmt);
        }

        var leftPad = function (n, pad) {
            n = "" + n;
            pad = "" + (pad == null ? "0" : pad);
            return n.length === 1 ? pad + n : n;
        };

        var r = [];
        var escape = false;

        if (monthNames == null) {
            monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
        }

        if (dayNames == null) {
            dayNames = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
        }

        for (var i = 0; i < fmt.length; ++i) {

            var c = fmt.charAt(i);

            if (escape) {
                switch (c) {
                    case "a": c = "" + dayNames[d.getDay()]; break;
                    case "b": c = "" + monthNames[d.getMonth()]; break;
                    case "d": c = leftPad(d.getDate()); break;
                    case "e": c = leftPad(d.getDate(), " "); break;
                    case "m": c = leftPad(d.getMonth() + 1); break;
                    case "y": c = leftPad(d.getFullYear() % 100); break;
                    case "Y": c = "" + d.getFullYear(); break;
                    case "w": c = "" + d.getDay(); break;
                }
                r.push(c);
                escape = false;
            } else {
                if (c === "%") {
                    escape = true;
                } else {
                    r.push(c);
                }
            }
        }
        return r.join("");
    }
    
    /* ---------------------------------------------------------------------------------------
     * Initialize the plugin
     * -------------------------------------------------------------------------------------*/
    function init(plot) {
        plot.hooks.processOptions.push(function (plot, options) {
            $.each(plot.getAxes(), function (axisName, axis) {

                var opts = axis.options;

                if (opts.mode === "time") {

                    axis.tickGenerator = function (axis) {

                        var ticks = [];
                        var span = axis.max - axis.min;

                        if (span < timeUnitSize.month) {
                            axis.tickSize = [1, "day"];
                        } else if (span > timeUnitSize.month && span < timeUnitSize.year) {
                            axis.tickSize = [1, "week"];
                        } else if (span >= timeUnitSize.year && span < (timeUnitSize.year * 3)) {
                            axis.tickSize = [1, "month"];
                        } else {
                            axis.tickSize = [1, "year"];
                        }

                        var d = new Date(axis.min);
                        var size = axis.tickSize[0];
                        var unit = axis.tickSize[1];
                        var step = size * timeUnitSize[unit];

                        // zero out the time parts since the smallest time increment will be a day
                        d.setMilliseconds(0); d.setSeconds(0); d.setMinutes(0); d.setHours(0);

                        var v = Number.NaN;
                        var prev;

                        do {
                            prev = v;
                            v = d.getTime();
                            ticks.push(v);
                            d.setTime(v + step);
                        } while (v < axis.max && v !== prev);

                        return ticks;
                    };

                    axis.tickFormatter = function (v, axis) {

                        var d = dateGenerator(v, axis.options);
                        var span = axis.max - axis.min;
                        var fmt;

                        if (span < timeUnitSize.month) {
                            fmt = "%a-%m/%d";
                        } else if (span > timeUnitSize.month && span < timeUnitSize.year) {
                            fmt = "%a-%m/%d"; //"%a-%m/%d/%y";
                        } else if (span >= timeUnitSize.year && span < (timeUnitSize.year * 3)) {
                            fmt = "%b %Y";
                        } else {
                            fmt = "%Y";
                        }

                        var rt = formatDate(d, fmt, opts.monthNames, opts.dayNames);

                        return rt;
                    };
                }
            });
        });
    }

    /* ---------------------------------------------------------------------------------------
     * Add plugin to the $.plot object
     * -------------------------------------------------------------------------------------*/
    $.plot.plugins.push({
        init: init,
        options: options,
        name: "FP-Time",
        version: "1.0"
    });

    /* ---------------------------------------------------------------------------------------
     * Time-axis support used to be in Flot core, which exposed the
     * formatDate function on the plot object.  Various plugins depend
     * on the function, so we need to re-expose it here.
     * -------------------------------------------------------------------------------------*/
    $.plot.formatDate = formatDate;
    $.plot.dateGenerator = dateGenerator;

})(jQuery);