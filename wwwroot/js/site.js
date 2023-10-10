// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#loadUsersBtn").click(function () {
    // ... seu código de carregamento de dados ...
    // No final, quando os dados são carregados:
    $("#cloneDataBtn").removeClass("disabled");
});

// Inicialmente, desabilite o botão:
$("#cloneDataBtn").addClass("disabled");
