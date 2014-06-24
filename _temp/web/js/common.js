//Config cookie
var minutes = 30;
var $expiration = new Date();
$expiration.setTime($expiration.getTime() + (minutes * 60 * 1000));
cookieConfig= {expires:$expiration, path:'/'};

$(document).ready(function(){
	//anima o menu do topo
	$(".quem-sou, .videos, .na-midia, .consulta-especial, .testes-online, .contato").hover(function(){
		var elem = $(this).children("span");
		var w = $(this).innerWidth();
		elem.animate({opacity:0},100,function(){
			$(".quem-sou span, .videos span, .na-midia span, .consulta-especial span, .testes-online span, .contato span").removeAttr("class").removeAttr("style");
			elem.addClass("over").css({opacity:1,width:0});
			elem.animate({width:w+'px'},1000);
		});
	},function(){
		var elem = $(this).children("span");
		elem.animate({opacity:0},200,function(){
			elem.removeAttr("class").removeAttr("style");
		});
	});
	//over imagens do menu lateral esquerda
	var options = {onError: function() {alert('ERROR');}};
	var effect = {vignette: 0,sepia: true,contrast: -10,desaturate:0.4,lighten:0,brightness:10};
	$('img.mini').each(function(){$(this).attr('alt',$(this).attr('src'));});
	$('img.mini').vintage(options, effect);
	$(".lat-esq .tecnicas li").hover(function(){
		var src=$(this).find(".mini").attr('alt');
		$(this).find(".mini").attr('alt',$(this).find(".mini").attr('src'));
		$(this).find(".mini").attr('src',src);
	},function(){
		var src=$(this).find(".mini").attr('alt');
		$(this).find(".mini").attr('alt',$(this).find(".mini").attr('src'));
		$(this).find(".mini").attr('src',src);
	});
	//focus no campo de busca
	$(document).on('focus', '#busca', function(e){ e.preventDefault(e);
		if($(this).val()=="O que você procura?"){$(this).val("")}
	});
	//blur no campo de busca
	$(document).on('blur', '#busca', function(e){ e.preventDefault(e);
		if($(this).val()==""){$(this).val("O que você procura?")}
	});
	//Seta propriedades do tamanho da font
	if($.cookie('textoSize')){sizeAttr($.cookie('textoSize'));}
	
	//resolucao de tela para mobile
//	layoutResponsivo($(window).width());
//	$(window).resize(function(e){layoutResponsivo($(this).width());});

});
/*
    function IEHoverPseudo() {

        var navItems = document.getElementById("navegacao-menu-topo").getElementsByTagName("li");

        for (var i = 0; i < navItems.length; i++) {
            if (navItems[i].className == "menuparent") {
                navItems[i].onmouseover = function () { this.className += " over"; }
                navItems[i].onmouseout = function () { this.className = "menuparent"; }
            }
        }

    }
    window.onload = IEHoverPseudo;
	*/



//Expandir mapa do site
function expand(){
	$(".btn-expand").slideToggle(500);
	$(".mapa-site").slideToggle(500);
}
//FontSize: Defini o tamanho padrao
function sizeDefault(){
	$.cookie('textoSize', 14, cookieConfig);
	sizeAttr(14);
}
//FontSize: Diminui
function sizeMenos(){
	if(!$.cookie('textoSize') || $.cookie('textoSize')==""){sizeDefault();}
	var SizeTemp=parseInt($.cookie('textoSize'))-2;
	if(SizeTemp>10){sizeAttr(SizeTemp);}
}
//FontSize: Aumenta
function sizeMais(){
	if(!$.cookie('textoSize') || $.cookie('textoSize')==""){sizeDefault();}
	var SizeTemp=parseInt($.cookie('textoSize'))+2;
	if(SizeTemp<26){sizeAttr(SizeTemp);}
}
//FontSize: Aplica o tamanho
function sizeAttr(size){
	$.cookie('textoSize', size, cookieConfig);
	$(".miolo .texto").animate({fontSize:size+'px'},300);
//	console.log(size);
}

function playmp3(){var audio = $('#audio');audio[0].play();
/*
	current = 0;
	playlist = $('#playlist');
	tracks = playlist.find('li a');
	len = tracks.length; //  - 1;
	//audio[0].volume = .10;
	playlist.find('a').click(function (e) {
		e.preventDefault();
		link = $(this);
		current = link.parent().index();
		run(link, audio[0]);
	});
	audio[0].addEventListener('ended', function (e) {
		current++;
		if (current == len) {
			current = 0;
			link = playlist.find('a')[0];
		} else {
			link = playlist.find('a')[current];
		}
		run($(link), audio[0]);
	});
*/
}
function stopmp3(){var audio = $('#audio');audio[0].pause();}
function run(link, player) {
	player.src = link.attr('href');
	par = link.parent();
	par.addClass('active').siblings().removeClass('active');
	audio[0].load();
	audio[0].play();
}
function log(str){console.log(str);}
//twitter-wjs
(function(d,s,id){
  var js,fjs=d.getElementsByTagName(s)[0];
  if(!d.getElementById(id)){
	  js=d.createElement(s);
	  js.id=id;
	  js.src="//platform.twitter.com/widgets.js";
	  fjs.parentNode.insertBefore(js,fjs);
  }
}(document,"script","twitter-wjs"));
//facebook-jssdk
(function(d,s,id) {
  var js, fjs = d.getElementsByTagName(s)[0];
  if (d.getElementById(id)) return;
  js = d.createElement(s); js.id = id;
  js.src = "//connect.facebook.net/pt_BR/all.js#xfbml=1&appId=198111923725028";
  fjs.parentNode.insertBefore(js,fjs);
}(document,'script','facebook-jssdk'));
//google plus
window.___gcfg = {lang: 'pt-BR'};
(function() {
  var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
  po.src = 'https://apis.google.com/js/platform.js';
  var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
})();


