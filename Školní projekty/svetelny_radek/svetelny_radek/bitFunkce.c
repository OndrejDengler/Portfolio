#include "bitFunkce.h"

//Nastaví daný bit do log. 1
int setBit(int value, int bitno) {
    value |= (1 << bitno);
    return value;
}

//Vynuluje daný bit
int clearBit(int value, int bitno) {
    value &= ~(1 << bitno);
    return value;
}

//Změna stavu daného bitu
int toggleBit(int value, int bitno) {
    value ^= (1 << bitno);
    return value;
}

//Vrátí true pokud je daný bit v log. 1
int bitIsSet(int value, int bitno) {
    return (value >> bitno) & 1;
}

//Vrátí true pokud je daný bit v log. 0 
int bitIsClear(int value, int bitno) {
    return !bitIsSet(value, bitno);
}