// Función para cargar la imagen en el modal y mostrarlo
function showImageInModal(imageUrl) {
    // Cambiar el atributo 'src' de la imagen dentro del modal
    document.getElementById('modalImage').src = imageUrl;
    // Mostrar el modal
    $('#imageModal').modal('show');
}
