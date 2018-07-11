///gui_element_define(type, args...);

//Enum used to mark element types
enum Element {
    Button,
    Break,
    Menu,
    Checkbox  
};

//Get script arguments.
var type = argument[0];
var arg;
for(var i = 1; i < argument_count; ++i) {
    arg[i-1] = argument[i];
}

var element;
    element[0] = type;
    
switch(type) {
    case Element.Break:
        return element;
    
    case Element.Button:
        element[1] = arg[0]; //Set element Name
        return element;
    
    case Element.Menu:
        element[1] = arg[0]; //Set element Name
        element[2] = arg[1]; //Set the menu this element opens.
        element[3] = -1;
        element[4] = -1;
        return element;
}
