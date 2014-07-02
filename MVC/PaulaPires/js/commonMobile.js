var navHistorico=["menuPrincipal"];

$(document).ready(function(){
	//over imagens do menu de tecnicas
	var options = {onError: function() {alert('ERROR');}};
	var effect = {vignette: 0,sepia: true,contrast: -10,desaturate:0.4,lighten:0,brightness:10};
	$('img.mini').vintage(options, effect);

	//focus no campo de busca
	$(document).on('focus', '#busca', function(e){ e.preventDefault(e);
		if($(this).val()=="O que você procura?"){$(this).val("")}
	});
	//blur no campo de busca
	$(document).on('blur', '#busca', function(e){ e.preventDefault(e);
		if($(this).val()==""){$(this).val("O que você procura?")}
	});

});

//navegacao do menu
function nav(itemMenu){
	var time=800;

	$("nav ul").animate({height:0,opacity:0},time,function(){$(this).removeAttr('style');});
	setTimeout(function(){
		$("ul."+itemMenu).fadeIn(time);
		if(itemMenu != "menuPrincipal"){$(".menu").fadeIn(200);}
		else{$(".menu").fadeOut(200);}
//		if(navHistorico[navHistorico.length-1]!=itemMenu){navHistorico.push(itemMenu);}
	},time);
}

function navVoltar(){
	if (navHistorico.length>0){
		navHistorico.splice(navHistorico.length-1);
		nav(navHistorico[navHistorico.length-1]);
	}
}

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
