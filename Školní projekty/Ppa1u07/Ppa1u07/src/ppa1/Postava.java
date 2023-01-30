package ppa1;

/***
 * @author Ondřej Dengler
 * @version 221120
 */
public class Postava {
	
	private String jmeno;
	private int sila;
	private int hbitost;
	private int vitalita;
	private int aktualniZdravi;
	private Zbran levaRuka;
	private Zbran pravaRuka;
	
	public String getJmeno() {
		return jmeno;
	}

	public void setJmeno(String jmeno) {
		this.jmeno = jmeno;
	}

	public int getSila() {
		return sila;
	}

	public void setSila(int sila) {
		this.sila = sila;
	}

	public int getHbitost() {
		return hbitost;
	}

	public void setHbitost(int hbitost) {
		this.hbitost = hbitost;
	}

	public int getVitalita() {
		return vitalita;
	}

	public void setVitalita(int vitalita) {
		this.vitalita = vitalita;
	}

	public int getAktualniZdravi() {
		return aktualniZdravi;
	}

	public void setAktualniZdravi(int aktualniZdravi) {
		this.aktualniZdravi = aktualniZdravi;
	}

	public Zbran getLevaRuka() {
		return levaRuka;
	}

	public void setLevaRuka(Zbran levaRuka) {
		this.levaRuka = levaRuka;
	}

	public Zbran getPravaRuka() {
		return pravaRuka;
	}

	public void setPravaRuka(Zbran pravaRuka) {
		this.pravaRuka = pravaRuka;
	}
	
	/**
	 * Konstruktor vytvoří instanci postavy se jménem, základní silou, hbitostí a vitalitou. 
	 */
	public Postava(String jmeno, int sila, int hbitost, int vitalita) {
		this.jmeno = jmeno;
		this.sila = sila;
		this.hbitost = hbitost;
		this.vitalita = vitalita;
		aktualniZdravi = vitalita;
	}
	
	/**
	 * Pokud postava nemá v ruce ruka zbraň, vezme ji a vrátí hodnotu true.
     * V opačném případě se nic nestane a vrátí hodnotu false. 
	 */
	public boolean vezmiZbran(Ruka ruka, Zbran zbran) { 
		if(ruka == Ruka.LEVA) {
			if(levaRuka!=null) { 
				return false;
			}
			else {
				levaRuka = zbran;
				return true;
			}
		}
		else if(ruka == Ruka.PRAVA) {
			if(pravaRuka!=null) {
				return false;
			}
			else {
				pravaRuka = zbran;
				return true;
			}
		}
		return false;
	}
	
	/**
	 * Sníží zdraví postavy v závislosti na síle útoku a aktuální obranné síle (tj. součet hbitosti postavy a obranné síly držených zbraní).
     * Aktuální zdraví se zmenší o rozdíl utok a aktuální obrana.
     * V případě slabého útoku (útok je menší než obrana) se zdraví nemění.
	 * @param utok v poskozeni
	 * @return Metoda vrátí množství ubraného zdraví. 
	 */
	public int branSe(int utok) {
		int poskozeni = utok - vypocetObrany();
		if(poskozeni < 0) {
			return 0;
		}
		else {
			aktualniZdravi -= poskozeni;
			return poskozeni;
		}
	}
	
	/**
	 * @return Vrátí celkovou útočnou sílu (tj. součet síly postavy a útočné síly držených zbraní).
	 */
	public int zautoc() {
		int celkovyPoskozeni = getSila();
		//Pokud postava nemá nic v levý ruce, nepřičte se žádný poškození.
		
		if(levaRuka!=null) {
			celkovyPoskozeni += levaRuka.getUtok();
		}
		//Pokud postava nemá nic v pravý ruce, nepřičte se žádný poškození.
		
		if(pravaRuka!=null) {
			celkovyPoskozeni += pravaRuka.getUtok();
		}
		return celkovyPoskozeni;
	}
	
	/**
	 * @return Pokud je aktuální zdraví větší než 0, postava je živá (tj. metoda vrátí true). 
	 */
	public boolean jeZiva() {
		if(getAktualniZdravi()>0) {
			return true;
		}
		else {
			return false;
		}
	}
	
	public String toString() {
		return getJmeno() + " [" + getAktualniZdravi() + "/" + getVitalita() + "] (" + zautoc() + "/" + vypocetObrany() + ")";
	}
	
	/**
	 * Metoda vypočítá celkovou obranu součtem obrany postavy a obran ze zbraní.
	 * @return celkovou obranu.
	 */
	private int vypocetObrany() {
		int celkovaObrana = getHbitost();
		// Pokud postava nemá nic v levý ruce, nepřičte se žádná obrana.
		if(levaRuka!=null) {
			celkovaObrana += levaRuka.getObrana();
		}
		// Pokud postava nemá nic v pravý ruce, nepřičte se žádná obrana.
		if(pravaRuka!=null) {
			celkovaObrana += pravaRuka.getObrana();
		}
		return celkovaObrana;
	}


}
