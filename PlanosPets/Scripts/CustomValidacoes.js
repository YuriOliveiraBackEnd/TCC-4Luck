// Aqui adicionamos os metodos respeitando os nomes gerado em nossa classe atributo
// os nomes devem estar em caixa baixa tanto na integração como aqui no JavaScript (Nome atributo, parametro)
$.validator.unobtrusive.adapters.addSingleVal('senhabrasil', 'params');

//Aqui temos a funcao que Valida do lado do cliente a senha digitadas utilizando uma expressão regular
$.validator.addMethod('senhabrasil',
    function (value, element, params) {

        // Separamos os parametros recebidos
        var parametros = params.split(';');

        // Definimos o tamanho maximo com base em parametro recebido      
        var maximo = parametros[1].toString();

        // Verifica se o tamanho maximo foi atingido
        if (value.length > maximo) {
            return false;
        }


        // Recupera expressao regular com base em parametro recebido
        var expReg = parametros[0];

        // Valida Expressao
        var result = value.match(expReg);

        // Retorna false caso a expressao nao seja valida
        if (result != null) {
            return true;
        }

        return false;
    });