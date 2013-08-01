// jquery文本框类css控制
$(document).ready(function () {

    $("input,textarea").focus(function () {
        $(this).addClass("sffocus");
    });
    $("input,textarea").blur(function () {
        $(this).removeClass("sffocus");
    });
});

//分页跳转
function gotoPage(urlStr) {
    //这里还需要实现一些对于输入页码的安全性验证。比如不能为空，必须是数字这些。
    var page = document.getElementById("PageInput").value;
    window.location.href = urlStr + "&page=" + page;
}

//打印
function Print() {
    bdhtml = window.document.body.innerHTML;
    sprnstr = "<!--startprint-->";
    eprnstr = "<!--endprint-->";
    prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
    prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
    window.document.body.innerHTML = prnhtml;
    window.print();
}

//添加收藏夹
function AddFavorite(sURL, sTitle) {
    try {
        window.external.addFavorite(sURL, sTitle);
    }
    catch (e) {
        try {
            window.sidebar.addPanel(sTitle, sURL, "");
        }
        catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}

//设为首页
function SetHome(obj, vrl) {
    try {
        obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(vrl);
    }
    catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            }
            catch (e) {
                alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入“about:config”并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', vrl);
        }
    }
}

/*
scaling     是否等比例自动缩放
width       图片最大高
height      图片最大宽
loadpic     加载中的图片路径
*/
(function ($) {
    jQuery.fn.LoadImage = function (settings) {
        settings = jQuery.extend({
            scaling: true,
            width: 500,
            height: 500,
            loadpic: ""
        }, settings);
        return this.each(function () {
            $.fn.LoadImage.Showimg($(this), settings);
        });
    };
    $.fn.LoadImage.Showimg = function ($this, settings) {
        var src = $this.attr("src");
        var img = new Image();
        img.src = src;
        var autoScaling = function () {
            if (settings.scaling) {
                if (img.width > 0 && img.height > 0) {
                    if (img.width / img.height >= settings.width / settings.height) {
                        if (img.width > settings.width) {
                            $this.width(settings.width);
                            $this.height((img.height * settings.width) / img.width);
                        }
                        else {
                            $this.width(img.width);
                            $this.height(img.height);
                        }
                    }
                    else {
                        if (img.height > settings.height) {
                            $this.height(settings.height);
                            $this.width((img.width * settings.height) / img.height);
                        }
                        else {
                            $this.width(img.width);
                            $this.height(img.height);
                        }
                    }
                }
            }
        }
        //正确处理自动读取缓存图片
        if (img.complete) {
            autoScaling();
            return;
        }
        $this.attr("src", "");

        var loading = $("<img alt=\"加载中…\" title=\"图片加载中…\" src=\"" + settings.loadpic + "\" />");
        $this.hide();
        $this.after(loading);
        $(img).load(function () {
            autoScaling();
            loading.remove();
            $this.attr("src", this.src);
            $this.fadeIn('slow');
        });
    }
})(jQuery);