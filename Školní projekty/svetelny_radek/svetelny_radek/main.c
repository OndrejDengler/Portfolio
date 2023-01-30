#include <avr/io.h>
#include <stdio.h>
#include <util/delay.h>
#include "bitFunkce.h"

int cisla[10][8] ={{0x00, 0x7C, 0xFE, 0x92, 0xA2, 0xFE, 0x7C, 0x00},	//Pole fontu èísel	
					{0x00, 0x02, 0x22, 0xFE, 0xFE, 0x02, 0x02, 0x00},	//øádky jsou èísla
					{0x00, 0x46, 0xCE, 0x8A, 0x92, 0xF2, 0x62, 0x00},	//sloupce odpovídají sloupcùm èísel
					{0x00, 0x44, 0xC6, 0x92, 0x92, 0xFE, 0x6C, 0x00},
					{0x00, 0x18, 0x18, 0x28, 0x68, 0xFE, 0xFE, 0x00},		
					{0x00, 0xE4, 0xE6, 0xA2, 0xA2, 0xBE, 0x9C, 0x00},
					{0x00, 0x7C, 0xFE, 0x92, 0x92, 0xDE, 0x4C, 0x00},		
					{0x00, 0xC0, 0xC0, 0x9E, 0xBE, 0xE0, 0xC0, 0x00},
					{0x00, 0x6C, 0xFE, 0x92, 0x92, 0xFE, 0x6C, 0x00},		
					{0x00, 0x64, 0xF6, 0x92, 0x92, 0xFE, 0x7C, 0x00}};

int cisloRegistr1 = 1; //Èísla, která se vykreslují na øádku
int cisloRegistr2 = 2;
int cisloRegistr3 = 3;
int cisloRegistr4 = 0;
int cisloRegistr5 = 4;
int cisloRegistr6 = 0;

void setup(void){ //Prvotní pøipravení portù
	DDRB = 0xFF;
	PORTB = 0xFF; //Data - AKTIVNÍ V 0
	DDRD = 0xFF;
	PORTD = 0x00; //Registry - AKTIVNÍ V 1
	_delay_ms(50);
}

void zapsaniDat(int cislo,int sloupec, int registr){ //Zapíše data na daný registr podle sloupce
	PORTB = 0xFF; //Vyèištìní dat
	PORTD = 0x00;
	PORTB = ~cisla[cislo][sloupec]; //Pøevrácení hodnot a pøíprava dat
	PORTD = setBit(0x00,registr); //Poslání dat na registr
	_delay_us(0.1);
	PORTD = 0x00;
	PORTB = 0xFF; //Vyèištìní dat
}

void vykresleniSloupce(int sloupec){ //Vykreslí zadaný sloupec na všech registrech
	PORTB = 0xFF; //Vyèištìní dat
	PORTD = setBit(0x00,7);
	_delay_us(1);
	sloupec = 7 - sloupec; //Obrácení sloupcù	
	PORTB = clearBit(0xFF, sloupec); //Urèení jaký sloupec
	PORTD = setBit(0x00,7); //Vykreslení sloupcù
	_delay_us(1);
	PORTD = 0x00;
	// doba zobr.
	//_delay_us(1);
	//Vyèištìní dat
}

void pocitaniCasu(void){ //Spravuje "tikání" èasu
	if(cisloRegistr6 != 9){ //Kontrola sekund druhé cifry
		cisloRegistr6++;
	}
	else{
		cisloRegistr5++; 
		cisloRegistr6 = 0;
	}
	if(cisloRegistr5 == 6){ //kontrola sekund první cifry
		cisloRegistr5 = 0;
		cisloRegistr6 = 0;
		cisloRegistr4++;
	}
	if(cisloRegistr4 == 10){ //kontrola minut druhé cifry
		cisloRegistr4=0;
		cisloRegistr3++;
	}
	if(cisloRegistr3 == 6){ //kontrola minut první cifry
		cisloRegistr3=0;
		cisloRegistr2++;
	}
	if(cisloRegistr2 == 10){ //kontrola hodin druhé cifry
		cisloRegistr2 = 0;
		cisloRegistr1++;
	}
	if((cisloRegistr1 == 2) && (cisloRegistr2 == 4)){ //kontrola hodin první cifry
		cisloRegistr1=0;
		cisloRegistr2=0;
	}
}

void vykresleniCisel(void){ //Funkce pro zapisování dat a jejich vykreslovení na øádku
	for(int y = 0; y<157;y++){
		for(int i = 0;i<8;i++){ //Pøíprava všech pøíslušných sloupcù a vykreslení sloupce
			zapsaniDat(cisloRegistr1,i,0); //Zapsání dat na i-tý sloupec první matice
			zapsaniDat(cisloRegistr2,i,1);
			zapsaniDat(cisloRegistr3,i,2);
			zapsaniDat(cisloRegistr4,i,3);
			zapsaniDat(cisloRegistr5,i,4);
			zapsaniDat(cisloRegistr6,i,5);
			vykresleniSloupce(i); //Vykreslení i-tého sloupce
			_delay_ms(2.5);
		}
	}
}

int main(void){
	setup();
	
	while(1){
		vykresleniCisel();
		pocitaniCasu();
	}
}