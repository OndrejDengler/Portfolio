package ppa1;

/***
 * @author Ondřej Dengler
 * @version 221120
 */
public class Zbran {
	
	private String nazev;
	private int utok;
	private int obrana;
	
	public String getNazev() {
		return nazev;
	}

	public void setNazev(String nazev) {
		this.nazev = nazev;
	}

	public int getUtok() {
		return utok;
	}

	public void setUtok(int utok) {
		this.utok = utok;
	}

	public int getObrana() {
		return obrana;
	}

	public void setObrana(int obrana) {
		this.obrana = obrana;
	}

	/**
	 * Konstruktor vytvoří zbraň s názvem , útokem a obranou. 
	 */
	public Zbran(String nazev, int utok, int obrana) {
		this.nazev = nazev;
		this.utok = utok;
		this.obrana = obrana;
	}
	
	public String toString() {
		return getNazev() + " (" + getUtok() + "/" + getObrana() + ")";
	}
}
