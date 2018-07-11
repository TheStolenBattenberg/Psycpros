///error_handle();

switch(global.iLastError) {
    case Error.WrongVersion:
        printf("Error! File is the wrong version -->");
        break;
        
    case Error.IO:
        printf("Error! I/O Operation failed -->");
        break;
        
    case Error.BadID:
        printf("Error! File has a bad ID -->");
        break;
        
    case Error.NoFile:
        printf("Error! File does not exist -->")
        break;
        
    default:
        return 1;
}
printf("            "+global.sErrorText);

global.iLastError = 0;
return -1;
