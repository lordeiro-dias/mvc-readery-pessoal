function realizarBuscaNoModal(event) {

    event.preventDefault(); 
    
    event.stopPropagation(); 
    
    const termoBusca = document.getElementById('nome-livro').value;
    const url = `/Livro/Index?busca=${encodeURIComponent(termoBusca)}`;

    fetch(url)
    .then(response => response.text())
    .then(html => {
        const parser = new DOMParser();
        const doc = parser.parseFromString(html, 'text/html');
        const novaLista = doc.getElementById('lista-livros-modal'); 
        
        if (novaLista) {
            document.getElementById('lista-livros-modal').innerHTML = novaLista.innerHTML;
        } else {
             console.warn("Elemento 'lista-livros-modal' não encontrado na resposta do servidor.");
        }
    })
    .catch(error => {
        console.error('Erro ao realizar a busca no modal:', error);
    });
}