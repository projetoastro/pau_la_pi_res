var navHistorico=["menuPrincipal"];

$(document).ready(function(){


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

//google analytics
(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
})(window,document,'script','//www.google-analytics.com/analytics.js','ga');

ga('create', 'UA-46955706-1', 'vidasutil.com.br');
ga('send', 'pageview');
