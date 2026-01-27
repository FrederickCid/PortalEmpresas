
const focuselement = (IdElement) =>{
    try {
        const element = document.getElementById(IdElement);
        if (element) {
            element.focus();
        }
    } catch (error) {
        console.error("Error focusing element:", error);
    }
}


const setValue = (IdElement,value) =>{
    try {
        const element = document.getElementById(IdElement);
        if (element) {
            element.value = value;
        }
    } catch (error) {
        console.error("Error focusing element:", error);
    }
}

const isFocused = (IdElement) => {
    try {
        const element = document.getElementById(IdElement);
        return element === document.activeElement;
    } catch (error) {
        console.error("Error checking focus:", error);
        return false;
    }
}

export { focuselement , setValue, isFocused};