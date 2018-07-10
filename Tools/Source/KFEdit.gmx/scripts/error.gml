///error(code);

switch(argument0) {
    case ERROR_OK: return 0;
    case ERROR_FAIL: show_message("Something went wrong. Closing."); game_end(); return 0;
    case ERROR_NOFILE: show_message("File does not exist!"); return 0;
    case ERROR_OUTOFBOUNDS: show_message("Tried to read outside the file."); return 0;
    case ERROR_BADID: show_debug_message("File has a bad magic number."); return 0;
}
return 0;
