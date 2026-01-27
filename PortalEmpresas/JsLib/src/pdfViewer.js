function mostrarPdfEnIframe(base64Data, id) {
    try {
        base64Data = base64Data.replace(/\s/g, '').replace(/[^A-Za-z0-9+/=]/g, '');
        const byteCharacters = atob(base64Data);
        const byteNumbers = Array.from(byteCharacters, c => c.charCodeAt(0));
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: 'application/pdf' });
        const blobUrl = URL.createObjectURL(blob);
        const iframe = document.getElementById(id);
        if (iframe) {
            iframe.src = blobUrl;
        } else {
            console.warn("❌ No se encontró el iframe con id:", id);
        }
    } catch (error) {
        console.error("❌ Error al procesar el base64:", error);
    }
}
window.mostrarPdfEnIframe = mostrarPdfEnIframe;

export { mostrarPdfEnIframe }