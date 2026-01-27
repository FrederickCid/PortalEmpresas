export const wasmReload = () => {
  return (
    window.location.reload()
  )
}

export function formatCLP(value) {
  const number = Math.round(Number(value) || 0);
  return `$${number.toLocaleString('es-CL')}`;
}