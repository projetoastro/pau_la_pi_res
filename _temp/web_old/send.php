<?php
header('Content-Type: text/html; charset=iso-8859-1');

function TrataAspas($campo){
	$campo = trim($campo);
	$campo = str_replace("'", "", $campo);
	$campo = str_replace("!", "", $campo);
	$campo = str_replace("<script", "", $campo);
	return $campo;
}

	
//funcao para enviar emails
function enviarEmail($para,$assunto,$msg){
	require_once("inc/class.phpmailer.php");
	//monta o email
	$mailer = new PHPMailer();	 
	$mailer->Subject = $assunto;	
	$mailer->Body = $msg;
	$mailer->IsHTML(true);
	//configura o SMTP
	$mailer->IsSMTP();
	$mailer->SMTPDebug = 1;
	$mailer->Port = 587; //Indica a porta de conexão para a saída de e-mails locawen 587
	$mailer->Host = 'smtp.vidasutil.com.br';
	$mailer->SMTPAuth = true; //define se haverá ou não autenticação no SMTP
	$mailer->Username = 'webmaster@vidasutil.com.br'; //Informe o e-mai o completo
	$mailer->Password = 'novasenha123'; //Senha da caixa postal
	$mailer->FromName = 'Paula Pires'; //Nome que será exibido para o destinatário
	$mailer->From = 'webmaster@vidasutil.com.br'; //Obrigatório ser a mesma caixa postal indicada em "username"
	$mailer->AddAddress("$para"); //Destinatários
	//envia
	if(!$mailer->Send()){
		return 1;
	}else{
		return 0;
	}	
}


$msg =	'<strong>Contato do site MOBILE</strong><br /><br />
<strong>Nome: </strong><br />'.TrataAspas($_POST["nome"]).'<br /><br />
<strong>E-mail: </strong><br />'.TrataAspas($_POST["email"]).'<br /><br />
<strong>Como tomou conhecimento do meu trabalho? </strong><br />'.TrataAspas($_POST["indicacao"]).'<br /><br />
<strong>Pretende marcar consulta como? </strong><br />'.TrataAspas($_POST["consulta"]).'<br /><br />
<strong>Mensagem: </strong><br />'.TrataAspas($_POST["mensagem"]).'<br /><br />';

//enviarEmail("heberth20@gmail.com","Mobile - Contato",$msg);
enviarEmail("vidasutil@vidasutil.com.br","Mobile - Contato",$msg);

//header("Location:index.html");
?>

<!DOCTYPE html>
<html>
<head>
    <title>Paula Pires - Contato</title>
    <meta name="keywords" content="terapeuta floral, florais, alquimia, massagem, massagista ayurvédica, terapeuta holística, reequilíbrio, mapa astral, astrologia, fitoterapia, acupuntura, farmacologia, meditação, astróloga, reeducadora alimentar, horóscopo, fitoterapeuta, florais brasileiros, terapia floral, massoterapeuta, alquimista, terapias alternativas, limpeza energética, acupunturista, espiritualidade, harmonia, equilíbrio, reeducação alimentar, Joel Aleixo, massagem ayurvédica, traumas, ansiedade, liberdade, transmutação, felicidade, alegria, chacras, alinhamento, transmutação, autoconhecimento, alma, espiritualidade, consciência, evolução, medicina, purificação, psicologia, elevação, espírito, auto-transformação" />
    <meta name="description" content="Site onde a Terapeuta Holística Paula Pires explica seu trabalho com as técnicas de Terapia Floral Alquímica, Reeducação Alimentar, Astrologia, Acupuntura, Fitoterapia, Massoterapia Ayurvédica e Meditação. Contém testes on-line, vídeos, áudios, textos e imagens. São Paulo - SP" />

    <meta name="Author" content="Kbytes Webstudio - Contato: heberth20@gmail.com">
    <meta name="copyright" content="Paula Pires - Todos os direitos reservados">
    <meta name="robots" content="index,follow">
    <meta name="revisit-after" content="7 Days">
    <meta http-equiv="imagetoolbar" content="false">
    <meta http-equiv="Content-Language" content="pt-br">
    <meta name="language" content="Portuguese">
    <meta charset="iso-8859-1">
	<meta name="viewport" content="width=device-width,user-scalable=no" media="(device-height: 568px)">
    
    <!-- Apple Web App -->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">
    <meta name="format-detection" content="telephone=no">
	
    <link rel="shortcut icon" href="img/favicon.ico" type="image/ico">
    <link rel="apple-touch-icon-precomposed" href="img/appicon57.png">
    <link rel="apple-touch-icon-precomposed" href="img/appicon57.png" sizes="76x76">
    <link rel="apple-touch-icon-precomposed" href="img/appicon114.png" sizes="114x114">
    <link rel="apple-touch-icon-precomposed" href="img/appicon152.png" sizes="152x152">

     <!-- Comum -->
    <link href="css/reset.css" media="all" rel="stylesheet" type="text/css">
    <link href="css/mobile.css" media="all" rel="stylesheet" type="text/css">
    
    <script type="text/javascript" src="js/lib/modernizr-2.6.2.min.js"></script>
    <script>
	alert('Obrigado pelo contato! Assim que possível entrarei em contato.');
	location.href='index.html';
	</script>
</head>
<body>
<section class="wrapper">
    <header>
        <div class="logo"><span class="paula">Paula</span> <span class="pires">Pires</span></div>
        <div class="menu-btn">
            <div class="menu"><a href="javascript:nav('menuPrincipal')">Menu</a></div>
            <div class="voltar" style="display:block"><a href="index.html">Voltar</a></div>
            <div class="cl"></div>
        </div>
        <nav>
            <h1 class="tit">Contato</h1>
        </nav>
    </header>
  
    <section class="content">
        <article class="miolo">
            <div class="texto">
                <form name="contato" method="post" action="send.php">
                    <fieldset>
                        <legend>Nome:</legend>
                        <input type="text" name="nome" class="required">
                    </fieldset>
                    <fieldset>
                        <legend>E-mail:</legend>
                        <input type="text" name="email" class="required email">
                    </fieldset>
                    <fieldset>
                        <legend>Como tomou conhecimento do meu trabalho?</legend>
                        <input type="text" name="indicacao">
                    </fieldset>
                    <fieldset>
                        <legend>Pretende marcar consulta como?</legend>
                        <input type="radio" name="consulta" value="Pessoalmente"> Pessoalmente<br>
                        <input type="radio" name="consulta" value="Skype"> Via Skype
                    </fieldset>
                    <fieldset>
                        <legend>Mensagem:</legend>
                        <textarea name="mensagem" class="required"></textarea>
                    </fieldset>
					<div class="enviar">
                		<input type="submit" name="enviar" value="Enviar >">
                	</div>
                </form>
            </div>
        </article>
    </section>
    
    <footer>
        <div class="aba">
            <img src="img/mobile/aba_esq.png" class="esq">
            <img src="img/mobile/aba_dir.png" class="dir">
            <div class="cl"></div>
        </div>
        <div class="content">            
            <div class="rede-focial">
                <a class="rede-yt">youtube</a>
                <a class="rede-fb">facebook</a>
                <a class="rede-tw">twitter</a>
                <a class="rede-it">intagram</a>
                <a class="rede-go">google plus</a>
            </div>
            <div class="cl"></div>
        </div>
    </footer>
</section>
<div class="aba2">&nbsp;</div>
<div class="rodape"><a href="mailto:paulapires@paulapires.com.br">Email:paulapires@paulapires.com.br</a></div>

</body>
</html>