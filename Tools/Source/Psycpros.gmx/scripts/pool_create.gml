///pool_create();

enum Pool {
    Data2D = $00,
    Data3D = $01
};

var pool;
    pool[0] = ds_list_create(); //List of files.
    pool[1] = ds_list_create(); //Pool Entries.
return pool;
