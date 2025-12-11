document.addEventListener('DOMContentLoaded', function(){
    const botoesAbrir = document.querySelectorAll('.abrir-modal-detalhe');
    const botoesFechar = document.querySelectorAll('.fechar-modal-detalhe');

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

    botoesFechar.forEach(function(botaofechar){
        botaofechar.addEventListener('click', function(e){
            const modal = e.target.closest('dialog');
            if(modal && typeof modal.close === 'function')
            {
                modal.close();
            }
        })
    })
})