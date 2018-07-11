///error_init();

enum Error {
    WrongVersion = -$04,
    NoFile       = -$03,
    BadID        = -$02,
    IO           = -$01
};

global.iLastError = 0;
global.sErrorText = "";
