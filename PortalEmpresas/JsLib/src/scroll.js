const scrollBottom = (element) =>{
    if(element)
        {
            element.scroollBottom = element.scrollHeight;
        }

}

const scrollTop = (element) =>{
    if(element)
        {
            element.scroollTop = element.scrollHeight;
        }

}

const scroll = (idWea) =>{
  const container = document.getElementById(idWea);
    if (container) {
      setTimeout(() => {
        container.scrollTop = container.scrollHeight;
      }, 50);
    }

}

export {scrollBottom, scrollTop, scroll};