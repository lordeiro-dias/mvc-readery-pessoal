// Pega os elementos basicos do modal
const modal = document.getElementById('modal-perfil');
const botaoAbrir = document.getElementById('abrir-modal');
const botaoFechar = document.getElementById('fechar-modal');
const botaoFechar2 = document.getElementById('fechar-modal2');

const overlay = document.getElementById('modal-overlay');

// ---------------------- Variaveis para Integracao de Dados (GET/POST) ----------------------
// DOM elements pros inputs do form
const nomeInput = document.getElementById('nome-usuario');
const generoSelect = document.getElementById('genero-usuario');
const descricaoTextarea = document.getElementById('campo-descricao');

// DOM elements pra foto
const fotoUsuarioImg = document.getElementById('foto-usuario');
const inputFotoFile = document.getElementById('input-foto');
const btnEnviarFoto = document.getElementById('enviar-foto-usuario');

// Botoes de acao
const btnSalvarMudancas = document.getElementById('salvar-mudancas');

// Constante pra rota do Controller
const URL_BASE = '/Livro';

// URL base do icone default pra resetar a foto
const FOTO_DEFAULT_URL = 'assets/icons/inserir-foto.svg';
const formElement = document.querySelector('.form-edicao-perfil');

/**
 * Reseta o formulario pra limpar qualquer alteracao nao salva 
 */
function resetarModal() {
    if (formElement) {
        formElement.reset();
    }
    // O reset() nao limpa o preview da imagem, entao a gente tem que resetar na mao.
    fotoUsuarioImg.src = FOTO_DEFAULT_URL;
    inputFotoFile.value = '';
}


// --- Listeners de Abertura ---
botaoAbrir.addEventListener('click', () => {
    // Abre o modal
    modal.classList.add('ativo');
    overlay.classList.add('ativo');

    // Chama a funcao pra puxar os dados do backend e preencher o form
    carregarDadosPerfil();
});


// --- Listeners de Fechamento ---
botaoFechar.addEventListener('click', () => {
    modal.classList.remove('ativo');
    overlay.classList.remove('ativo');
    resetarModal();
});

botaoFechar2.addEventListener('click', () => {
    modal.classList.remove('ativo');
    overlay.classList.remove('ativo');
    resetarModal();
});

overlay.addEventListener('click', () => {
    modal.classList.remove('ativo');
    overlay.classList.remove('ativo');
    resetarModal();
});

document.addEventListener('keydown', ESC => {
    if (ESC.key === 'Escape') {
        modal.classList.remove('ativo');
        overlay.classList.remove('ativo');
        resetarModal();
    }
});


/**
 * GET: Carrega dados do perfil (Nome, Genero, Descricao, FotoURL) do C#
 * Rota: LivroController.BuscarPerfil
 */
function carregarDadosPerfil() {
    fetch(`${URL_BASE}/BuscarPerfil`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Falha na requisicao GET de perfil.');
            }
            return response.json();
        })
        .then(data => {
            if (!data) return;

            // Popula os campos com os dados do banco
            nomeInput.value = data.nome || '';
            // OK: Corrigido para data.descricao
            descricaoTextarea.value = data.descricao || '';
            // OK: Corrigido para data.genero
            generoSelect.value = data.genero || 'Outro';

            // Verifica e exibe a foto (Base64)
            if (data.fotoURL) {
                if (data.fotoURL.length > 500) {
                    fotoUsuarioImg.src = `data:image/jpeg;base64,${data.fotoURL}`;
                } else {
                    fotoUsuarioImg.src = data.fotoURL;
                }
            } else {
                fotoUsuarioImg.src = FOTO_DEFAULT_URL;
            }
        })
        .catch(error => {
            console.error('Erro GET no perfil:', error);
        });
}


/**
 * Logica pra abrir o file explorer quando clica no botao de 'Fazer upload' 
 * e mostrar o preview da imagem selecionada.
 */
btnEnviarFoto.addEventListener('click', () => {
    inputFotoFile.click();
});

inputFotoFile.addEventListener('change', function (event) {
    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            fotoUsuarioImg.src = e.target.result;
        };
        reader.readAsDataURL(file);
    }
});


/**
 * POST: Salva as alteracoes (Nome, Genero, Descricao, Foto).
 * Rota: LivroController.AtualizarPerfil
 */
btnSalvarMudancas.addEventListener('click', (event) => {
    event.preventDefault();

    const formData = new FormData();

    // Anexa dados de texto (Tem que bater com os 'Names' do PerfilViewModel no C#)
    formData.append('Nome', nomeInput.value);
    // OK: Corrigido para 'Genero'
    formData.append('Genero', generoSelect.value);
    // OK: Corrigido para 'Descricao'
    formData.append('Descricao', descricaoTextarea.value);

    // Anexa arquivo de foto, se tiver sido selecionado um novo
    const fotoFile = inputFotoFile.files[0];
    if (fotoFile) {
        formData.append('Foto', fotoFile);
    }

    fetch(`${URL_BASE}/AtualizarPerfil`, {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (response.ok || response.redirected) {
                alert('Perfil atualizado com sucesso! Recarregando...');

                // Fecha modal
                modal.classList.remove('ativo');
                overlay.classList.remove('ativo');

                // Recarrega a pagina.
                window.location.reload();
            } else {
                throw new Error('Falha ao salvar. Status: ' + response.status);
            }
        })
        .catch(error => {
            console.error('Erro POST ao salvar:', error);
            alert('Erro critico ao salvar: ' + error.message + '. Tente de novo.');
        });
});