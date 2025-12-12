
function abrirModalExcluir(livrinhoId) { 

    const seletorModalDetalhe = `.modal-detalhe-livro-${livrinhoId}`;
    const modalDetalheAberto = document.querySelector(seletorModalDetalhe);
    
    if (modalDetalheAberto && typeof modalDetalheAberto.close === 'function') {
        modalDetalheAberto.close();
    }

    const seletorModalExclui = `.modal-detalhe-exclui-${livrinhoId}`;
    const modalDesejadoExclui = document.querySelector(seletorModalExclui);

    if (modalDesejadoExclui && typeof modalDesejadoExclui.showModal === 'function') {
        modalDesejadoExclui.showModal();
    } else {
        console.error(`Modal de exclusão da Estante não encontrado para o ID: ${livrinhoId}`);
    }
}

function abrirModalExcluirCatalogo(livroId, estanteId) {

    const seletorModalExcluiEstante = `.modal-detalhe-exclui-${estanteId}`;
    const modalEstanteAberto = document.querySelector(seletorModalExcluiEstante);
    
    if (modalEstanteAberto && typeof modalEstanteAberto.close === 'function') {
        modalEstanteAberto.close();
    }

    const seletorModalCatalogo = `.modal-detalhe-catalogo-${livroId}`;
    const modalDesejadoCatalogo = document.querySelector(seletorModalCatalogo);

    if (modalDesejadoCatalogo && typeof modalDesejadoCatalogo.showModal === 'function') {
        modalDesejadoCatalogo.showModal();
    } else {
        console.error(`Modal de exclusão do Catálogo não encontrado para o ID do livro: ${livroId}`);
    }
}


document.addEventListener('DOMContentLoaded', function(){
    const botoesAbrir = document.querySelectorAll('.abrir-modal-detalhe');
    const botoesFecharDetalhe = document.querySelectorAll('.fechar-modal-detalhe');
    const botaoFechaModalExcluir = document.querySelectorAll('.botao-fecha-modal-exclui');
    const botaoFechaModalCatalogo = document.querySelectorAll('.botao-fecha-modal-catalogo');

    botoesAbrir.forEach(function(botao){
        botao.addEventListener('click', function(){
            const estanteId = botao.getAttribute('data-modal-id');
            const seletorModal = `.modal-detalhe-livro-${estanteId}`;
            const modalDesejado = document.querySelector(seletorModal);

            if(modalDesejado && typeof modalDesejado.showModal === 'function'){
                modalDesejado.showModal();
            } 
            else{
                console.error(`Modal não encontrado ou showModal indisponível para o seletor: ${seletorModal}`)
            }
        })
    })

    botoesFecharDetalhe.forEach(function(botaofechar){
        botaofechar.addEventListener('click', function(e){
            const modal = e.target.closest('dialog');
            if(modal && typeof modal.close === 'function')
            {
                modal.close();
            }
        })
    })

    botaoFechaModalExcluir.forEach(function(botaofechar){
        botaofechar.addEventListener('click', function(e){
            const modalId = botaofechar.getAttribute('data-modal-id');
            const seletorModal = `.modal-detalhe-exclui-${modalId}`;
            const modal = document.querySelector(seletorModal);
            if(modal && typeof modal.close === 'function')
            {
                modal.close();
            }
        })
    })

    botaoFechaModalCatalogo.forEach(function(botaofechar){
        botaofechar.addEventListener('click', function(e){
            const modal = e.target.closest('dialog');
            if(modal && typeof modal.close === 'function')
            {
                modal.close();
            }
        })
    })
});