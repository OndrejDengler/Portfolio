#include <avr/io.h>
#include <stdio.h>
#include <util/delay.h>
#include "bitFunkce.h"

int cisla[10][8] ={{0x00, 0x7C, 0xFE, 0x92, 0xA2, 0xFE, 0x7C, 0x00},	//Pole fontu ��sel	
					{0x00, 0x02, 0x22, 0xFE, 0xFE, 0x02, 0x02, 0x00},	//��dky jsou ��sla
					{0x00, 0x46, 0xCE, 0x8A, 0x92, 0xF2, 0x62, 0x00},	//sloupce odpov�daj� sloupc�m ��sel
					{0x00, 0x44, 0xC6, 0x92, 0x92, 0xFE, 0x6C, 0x00},
					{0x00, 0x18, 0x18, 0x28, 0x68, 0xFE, 0xFE, 0x00},		
					{0x00, 0xE4, 0xE6, 0xA2, 0xA2, 0xBE, 0x9C, 0x00},
					{0x00, 0x7C, 0xFE, 0x92, 0x92, 0xDE, 0x4C, 0x00},		
					{0x00, 0xC0, 0xC0, 0x9E, 0xBE, 0xE0, 0xC0, 0x00},
					{0x00, 0x6C, 0xFE, 0x92, 0x92, 0xFE, 0x6C, 0x00},		
					{0x00, 0x64, 0xF6, 0x92, 0x92, 0xFE, 0x7C, 0x00}};

int cisloRegistr1 = 1; //��sla, kter� se vykresluj� na ��dku
int cisloRegistr2 = 2;
int cisloRegistr3 = 3;
int cisloRegistr4 = 0;
int cisloRegistr5 = 4;
int cisloRegistr6 = 0;

void setup(void){ //Prvotn� p�ipraven� port�
	DDRB = 0xFF;
	PORTB = 0xFF; //Data - AKTIVN� V 0
	DDRD = 0xFF;
	PORTD = 0x00; //Registry - AKTIVN� V 1
	_delay_ms(50);
}

void zapsaniDat(int cislo,int sloupec, int registr){ //Zap�e data na dan� registr podle sloupce
	PORTB = 0xFF; //Vy�i�t�n� dat
	PORTD = 0x00;
	PORTB = ~cisla[cislo][sloupec]; //P�evr�cen� hodnot a p��prava dat
	PORTD = setBit(0x00,registr); //Posl�n� dat na registr
	_delay_us(0.1);
	PORTD = 0x00;
	PORTB = 0xFF; //Vy�i�t�n� dat
}

void vykresleniSloupce(int sloupec){ //Vykresl� zadan� sloupec na v�ech registrech
	PORTB = 0xFF; //Vy�i�t�n� dat
	PORTD = setBit(0x00,7);
	_delay_us(1);
	sloupec = 7 - sloupec; //Obr�cen� sloupc�	
	PORTB = clearBit(0xFF, sloupec); //Ur�en� jak� sloupec
	PORTD = setBit(0x00,7); //Vykreslen� sloupc�
	_delay_us(1);
	PORTD = 0x00;
	// doba zobr.
	//_delay_us(1);
	//Vy�i�t�n� dat
}

void pocitaniCasu(void){ //Spravuje "tik�n�" �asu
	if(cisloRegistr6 != 9){ //Kontrola sekund druh� cifry
		cisloRegistr6++;
	}
	else{
		cisloRegistr5++; 
		cisloRegistr6 = 0;
	}
	if(cisloRegistr5 == 6){ //kontrola sekund prvn� cifry
		cisloRegistr5 = 0;
		cisloRegistr6 = 0;
		cisloRegistr4++;
	}
	if(cisloRegistr4 == 10){ //kontrola minut druh� cifry
		cisloRegistr4=0;
		cisloRegistr3++;
	}
	if(cisloRegistr3 == 6){ //kontrola minut prvn� cifry
		cisloRegistr3=0;
		cisloRegistr2++;
	}
	if(cisloRegistr2 == 10){ //kontrola hodin druh� cifry
		cisloRegistr2 = 0;
		cisloRegistr1++;
	}
	if((cisloRegistr1 == 2) && (cisloRegistr2 == 4)){ //kontrola hodin prvn� cifry
		cisloRegistr1=0;
		cisloRegistr2=0;
	}
}

void vykresleniCisel(void){ //Funkce pro zapisov�n� dat a jejich vykresloven� na ��dku
	for(int y = 0; y<157;y++){
		for(int i = 0;i<8;i++){ //P��prava v�ech p��slu�n�ch sloupc� a vykreslen� sloupce
			zapsaniDat(cisloRegistr1,i,0); //Zaps�n� dat na i-t� sloupec prvn� matice
			zapsaniDat(cisloRegistr2,i,1);
			zapsaniDat(cisloRegistr3,i,2);
			zapsaniDat(cisloRegistr4,i,3);
			zapsaniDat(cisloRegistr5,i,4);
			zapsaniDat(cisloRegistr6,i,5);
			vykresleniSloupce(i); //Vykreslen� i-t�ho sloupce
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