#include <avr/io.h>
#include <stdio.h>
#include <util/delay.h>
#include "bitFunkce.h"

void setup(void){
	DDRB = 0xFF;
	PORTB = 0x00; //Ovládání znaku
	DDRD = 0xFF;
	PORTD = 0x00; //Ovládání pozice+Dvere
	DDRF = 0x00; //Klávesnice
	_delay_ms(50);
}

void znak(int cislo){
	PORTB = cislo;
}

void cifra(int pozice){
	PORTD = pozice;
}

void zmenaCifry(int momentalniPozice){
	int pozice = 0xFF;
	if(momentalniPozice == 4){
		pozice = clearBit(pozice,0);
		cifra(pozice);
	}
	else{
		pozice = clearBit(pozice,momentalniPozice);
		cifra(pozice);
	}
	
}

int main(void){
	setup();
	int kratkyDelay = 1;
	int delsiDelay = 250;
	int nasePozice = 0;
	int potvrzeni = 0;
	int zadany[4] = {1,1,1,1};
	int heslo[4] = {1,1,1,1};
	
	
	while(1){
		for(int i = 0;i<4;i++){
			if(bitIsClear(PINF,1)){ //Zjištìní zda je tlaèítko zmáèknuto
				if(i == 1){ //První tlaèítko
					_delay_ms(delsiDelay);
					if(nasePozice == 3){
						nasePozice = 0;
					}
					else{
						nasePozice++;
					}
					
				}
				if(i == 2){ //Druhé tlaèítko
					_delay_ms(delsiDelay);
					if(zadany[nasePozice] == 9){ //Pokud je èíslo 9 pøepíše se na 0
						zadany[nasePozice] = 0;
					}
					else{
						zadany[nasePozice]++;
					}
				}
				if(i == 3){ //Tøetí tlaèítko
					_delay_ms(delsiDelay);
					if(zadany[nasePozice] == 0){ //Pokud je èíslo 0 pøepíše se na 9
						zadany[nasePozice] = 9;
					}
					else{
						zadany[nasePozice]--;
					}
				}
				else{ //Ètvrté tlaèítko
					_delay_ms(delsiDelay);
					potvrzeni = 0;
					for(int y = 0;y<4;y++){
						if(zadany[nasePozice] == heslo[i]){ //Kontrola každé cifry
							potvrzeni++;
						}
					}
					if(potvrzeni == 4){ //Pokud jsou všechny OK, otevøe dveøe
						PORTD = clearBit(PORTD,4);
						_delay_ms(delsiDelay);
					}
					PORTD = setBit(PORTD,4);
				}
			}
			zmenaCifry(i); //Vypisovani cisel na displej
			znak(zadany[i]);
			_delay_ms(kratkyDelay);
		}
	}
}
