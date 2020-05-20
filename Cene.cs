using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivosCalc
{
    public class Cene
    {
        public float _cenaStebraNaMeter;
        public float _kratekNosilec;
        public float _dolgNosilec;
        public float _rozeta;
        public float _pokrov;
        public float _lepilo;
        public float _enSraub;
        public float _montaza;
        public float _montazaStebra;
        public float _razrez;
        public float _pokrovZaRocaj;
        public float _rocaj;
        public float _lprofil;
        public float _vodilo;
        public Cene(float cenaStebraNaMeter, float kratekNosilec, float dolgNosilec, float rozeta, float pokrov, float lepilo, float montazaStebra)
        {
            this._cenaStebraNaMeter = cenaStebraNaMeter;
            this._kratekNosilec = kratekNosilec;
            this._dolgNosilec = dolgNosilec;
            this._rozeta = rozeta;
            this._pokrov = pokrov;
            this._lepilo = lepilo;
            this._montazaStebra = montazaStebra;
        }
        public Cene(float enSraub, float montaza, float razrez)
        {
            this._enSraub = enSraub;
            this._montaza = montaza;
            this._razrez = razrez;
        }

        public Cene(float lepilo)
        {
            this._lepilo = lepilo;
        }

        public Cene(float pokrovZaRocaj, float rocaj, float lprofil, float vodilo)
        {
            this._pokrovZaRocaj = pokrovZaRocaj;
            this._rocaj = rocaj;
            this._lprofil = lprofil;
            this._vodilo = vodilo;
        }
    }
}
