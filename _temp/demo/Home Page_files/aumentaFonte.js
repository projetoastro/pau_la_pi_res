function aumenta_fonte() {

    if (window.name == "") {
        var tamOrigin = 14;
    } else {
        var tamOrigin = window.name;
    }


    $('#aumentaFonte').click(function () {

        if (tamOrigin == 14) {

            tamOrigin = 16;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
            window.name = tamOrigin;
        } else if (tamOrigin == 16) {
            tamOrigin = 18;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
            window.name = tamOrigin;
        } else if (tamOrigin == 18) {
            tamOrigin = 20;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
            window.name = tamOrigin;
        } else if (tamOrigin == 20) {
            tamOrigin = 22;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
            window.name = tamOrigin;
        } else if (tamOrigin == 22) {
            tamOrigin = 24;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
            window.name = tamOrigin;
        } else if (tamOrigin == 24) {
            tamOrigin = 26;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
            window.name = tamOrigin;
        }

        window.name = "";
    });

    $('#diminuiFonte').click(function () {



        if (tamOrigin == 26) {
            tamOrigin = 24;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
        } else if (tamOrigin == 24) {
            tamOrigin = 22;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
        } else if (tamOrigin == 22) {
            tamOrigin = 20;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
        } else if (tamOrigin == 20) {
            tamOrigin = 18;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
        } else if (tamOrigin == 18) {
            tamOrigin = 16;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
        } else if (tamOrigin == 16) {
            tamOrigin = 14;
            $('.texto-chamada p').css('font-size', tamOrigin + 'px');
        }
        window.name = "";
    });



}