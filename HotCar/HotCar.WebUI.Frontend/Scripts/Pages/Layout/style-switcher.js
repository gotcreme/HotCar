! function () {
    var t;
    $(document).ready(function () {
        var a, e, s, o;
        return e = "../Content/jednotka/", o = $("#style-switcher"), o.find(".fa-icon-cogs").click(function () {
            return o.toggleClass("open")
        }), o.find(".style-switcher-color").click(function () {
            var a, s, o;
            return a = $(this), o = $(a.data("switch-target")), s = $(this).data("switch-to"), o.attr("href", "" + e + s), localStorage.setItem("colors", s), t(a), !1
        }), o.find(".style-switcher-button").click(function () {
            var a, e, s;
            switch (e = $(this), e.parent().hasClass("style-switcher-reset") || t(e), a = e.data("switch-to").split(":"), s = $(e.data("switch-target")), a[0]) {
                case "addClass":
                    s.addClass(a[1]), localStorage.setItem("layout", a[1]);
                    break;
                case "removeClass":
                    s.removeClass(a[1]), localStorage.removeItem("layout", a[1]);
                    break;
                case "reset":
                    s.find("[data-switch-default]").click(), localStorage.removeItem("colors"), localStorage.removeItem("layout")
            }
            return !1
        }), a = localStorage.getItem("colors"), null !== a && o.find(".style-switcher-color[data-switch-to='" + a + "']").length > 0 && ($("#colors").attr("href", "" + e + a), t(o.find("[data-switch-to='" + a + "']"))), s = localStorage.getItem("layout"), null !== s && o.find(".style-switcher-button[data-switch-to='addClass:" + s + "']").length > 0 ? ($("body").addClass(s), t(o.find("[data-switch-to='addClass:" + s + "']"))) : void 0
    }), t = function (t) {
        return t.parent().find("a").removeClass("active"), t.addClass("active"), t
    }
}.call(this),
    function () {
        $(document).ready(function () {
            return $("#header .navbar-brand").hover(function () {
                return $(this).toggleClass("tada animated")
            })
        })
    }.call(this);