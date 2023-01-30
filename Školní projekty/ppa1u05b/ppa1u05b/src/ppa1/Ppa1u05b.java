package ppa1;

import java.util.Scanner;

public class Ppa1u05b {

	public static void main(String[] args) {
		Scanner scn = new Scanner(System.in);
		
		System.out.print("c: ");
		int numberC = scn.nextInt();
		boolean dividible = false;
		for (int numberB = 2; numberB < numberC; numberB++) { 
			for (int numberA = 1; numberA < numberB; numberA++) {
				
				if((Math.pow(numberA, 2) + Math.pow(numberB, 2)) == Math.pow(numberC, 2)) { //Kontrola jestli rovnost plati
					for (int j = 2; j < numberA; j++) { //Kontrola jestli cisla maji stejneho delitele
						if(numberA%j == 0 && numberB%j==0 && numberC%j == 0) {
							dividible=true;
						}
					}
					if(!dividible) {
						System.out.printf("%d^2+%d^2=%d^2",numberA,numberB,numberC);
						return;
					}
					dividible = false;
				}
			}
		}
		System.out.println("Reseni neexistuje.");
	}
}
