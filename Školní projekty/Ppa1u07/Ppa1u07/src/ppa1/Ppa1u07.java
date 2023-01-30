package ppa1;

import java.util.Scanner;

/***
 * @author Ondřej Dengler
 * @version 221120
 */
public class Ppa1u07 {

	public static void main(String[] args) {
		Scanner sc = new Scanner(System.in);
		
		Postava postava1 = nactiPostavu(sc);
		vyzbrojPostavu(postava1, nactiZbran(sc), nactiZbran(sc));
		
		Postava postava2 = nactiPostavu(sc);
		vyzbrojPostavu(postava2, nactiZbran(sc), nactiZbran(sc));
		
		Postava vyherce = souboj(postava1,postava2);
		
		System.out.println("Vitez: " + vyherce.toString());
	}
	
	/**
	 * Z předaného Scanneru načte postupně jméno, sílu, hbitost a zdraví a vytvoří novou postavu. 
	 * @return načtenou postavu.
	 */
	public static Postava nactiPostavu(Scanner sc) {
		String jmeno = sc.nextLine();
		int sila = Integer.parseInt(sc.nextLine());
		int hbitost = Integer.parseInt(sc.nextLine());
		int vitalita = Integer.parseInt(sc.nextLine());
		
		return new Postava(jmeno,sila,hbitost,vitalita);
	}
	
	/**
	 * Z předaného Scanneru načte postupně název, útočnou a obrannou sílu a vytvoří novou zbraň. 
	 * Pokud se zadá prázdný název, metoda okamžitě vrátí hodnotu null (zbytek informací nenačítá).
	 * @return načtenou zbran.
	 */
	public static Zbran nactiZbran(Scanner sc) {
		String nazev = sc.nextLine();
		if(nazev == "") {
			return null;
		}
		
		int utok = Integer.parseInt(sc.nextLine());
		int obrana = Integer.parseInt(sc.nextLine());
		
		return new Zbran(nazev,utok,obrana);
	}
	
	/**
	 * Vyzbrojí postavu dodanými zbraněmi.
	 * Pokud již postava nějakou zbraň měla, zůstane jí 
	 */
	public static void vyzbrojPostavu(Postava postava, Zbran leva, Zbran prava) {
		postava.vezmiZbran(Ruka.LEVA, leva);
		postava.vezmiZbran(Ruka.PRAVA, prava);
	}
	
	/**
	 * Spustí souboj mezi postavami.
     * Souboj končí v okamžiku, kdy aktuální zdraví některé z postav klesne pod 1.
     * Útočit začíná postava1.
	 * @return Metoda vrátí přeživší postavu.
	 */
	public static Postava souboj(Postava postava1, Postava postava2) {
		while(postava1.jeZiva() && postava2.jeZiva()) {
			if(postava1.jeZiva()) {
				postava2.branSe(postava1.zautoc());
			}
			if(postava2.jeZiva()) {
				postava1.branSe(postava2.zautoc());
			}
		}
		return postava1.jeZiva() ? postava1 : postava2;
	}

}
