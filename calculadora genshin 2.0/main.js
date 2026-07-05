fetch("navbar.html")
      .then(res => res.text())
      .then(html => {
        document.getElementById("navbar-container").innerHTML = html;

        // Resalta el enlace activo según la página actual
        const links = document.querySelectorAll(".nav-link");
        links.forEach(link => {
          if (link.href === window.location.href) {
            link.classList.add("active");
          }
        });

        // Muestra "Login" o "Cerrar sesión (usuario)" según corresponda
        if (typeof updateNavbarAuth === "function") updateNavbarAuth();
      });
 
function toggleSeccion(){
    const seccion = document.getElementById("Calculadora_2");
    seccion.classList.toggle("visible");
}

// ══════════════════════════════════════════════════════════════
//  LÓGICA DE CÁLCULO (calculator.js integrado)
// ══════════════════════════════════════════════════════════════

const ELEMENTS = ["Physical","Pyro","Hydro","Electro","Anemo","Cryo","Geo","Dendro"];

const ELEMENT_COLORS = {
  Physical: "#b0b8c8",
  Pyro:     "#ff6b35",
  Hydro:    "#4fc3f7",
  Electro:  "#ce93d8",
  Anemo:    "#80cbc4",
  Cryo:     "#90caf9",
  Geo:      "#ffcc02",
  Dendro:   "#aed581",
};

const ELEMENT_ICONS = {
  Physical: "⚪", Pyro: "🔥", Hydro: "💧", Electro: "⚡",
  Anemo: "🌪️", Cryo: "❄️", Geo: "🪨", Dendro: "🌿",
};

const TRANSFORMATIVE_REACTIONS = {
  None:          { label: "— Ninguna —",           mult: 0 },
  Burning:       { label: "Burning 🔥",            mult: 0.25 },
  Superconduct:  { label: "Superconduct ⚡❄️",     mult: 0.5 },
  Swirl:         { label: "Swirl 🌪️",              mult: 0.6 },
  ElectroCharged:{ label: "ElectroCharged ⚡💧",   mult: 1.2 },
  Shattered:     { label: "Shattered 💥",          mult: 3.0 },
  Overloaded:    { label: "Overloaded 🔥⚡",       mult: 4.0 },
  Bloom:         { label: "Bloom 🌿💧",            mult: 4.0 },
  Hyperbloom:    { label: "Hyperbloom 🌿⚡",       mult: 6.0 },
  Burgeon:       { label: "Burgeon 🌿🔥",          mult: 6.0 },
};

const AMPLIFYING_REACTIONS = {
  None:           { label: "— Ninguna —",                       mult: 1.0 },
  MeltStrong:     { label: "Melt Fuerte (Pyro→Cryo) 🔥❄️",    mult: 2.0 },
  MeltWeak:       { label: "Melt Débil (Cryo→Pyro) ❄️🔥",     mult: 1.5 },
  VaporizeStrong: { label: "Vaporize Fuerte (Hydro→Pyro) 💧🔥",mult: 2.0 },
  VaporizeWeak:   { label: "Vaporize Débil (Pyro→Hydro) 🔥💧", mult: 1.5 },
};

const SKILL_TYPES = {
  Attack: "⚔️ Ataque",
  Support: "🛡️ Soporte",
};

function calcTransformativeBonus(reactionKey, em) {
  const r = TRANSFORMATIVE_REACTIONS[reactionKey];
  if (!r || r.mult === 0) return 0;
  const BASE = 1446.85;
  const emBonus = (16 * em) / (em + 2000);
  return r.mult * BASE * (1 + emBonus);
}

function calcAmplifyingMult(reactionKey, em) {
  const r = AMPLIFYING_REACTIONS[reactionKey];
  if (!r) return 1.0;
  if (r.mult === 1.0) return 1.0;
  const emBonus = (2.78 * em) / (em + 1400);
  return r.mult * (1 + emBonus);
}

function calcAttackSkillDamage(skill, stats, enemyLevel, baseEnemyRes, supportBuffs) {
  let effectiveRes = Math.max(-1, baseEnemyRes - (supportBuffs.resReduction || 0));

  const scalingBase =
    stats.baseATK * (skill.motionValue / 100) +
    stats.baseHP  * (skill.hpScaling  / 100) +
    stats.baseDEF * (skill.defScaling / 100) +
    stats.em      * (skill.emScaling  / 100);

  const dmgBonus = skill.element === "Physical"
    ? stats.physDmgBonus / 100
    : stats.elemDmgBonus / 100;

  const supportDmgAmp = 1 + (supportBuffs.dmgBoost || 0) / 100;

  const charLvl = 90;
  const defMult = (charLvl + 100) / ((charLvl + 100) + (enemyLevel + 100));

  let resMult;
  if (effectiveRes < 0)        resMult = 1 - effectiveRes / 2;
  else if (effectiveRes < 0.75) resMult = 1 - effectiveRes;
  else                          resMult = 1 / (4 * effectiveRes + 1);

  const baseDmgPerHit = scalingBase * (1 + dmgBonus) * defMult * resMult * supportDmgAmp;
  const ampMult = calcAmplifyingMult(skill.ampReaction, stats.em);

  const baseDmg = baseDmgPerHit * ampMult;
  const critDmg = baseDmg * (1 + stats.critDmg / 100);
  const cr      = Math.min(1, Math.max(0, stats.critRate / 100));
  const avgDmg  = baseDmg * (1 + cr * (stats.critDmg / 100));

  const transBonus = calcTransformativeBonus(skill.transReaction, stats.em);
  const totalAvg   = (avgDmg + transBonus) * skill.hits;

  return {
    baseDmg:     baseDmg  * skill.hits,
    critDmg:     critDmg  * skill.hits,
    avgDmg:      avgDmg   * skill.hits,
    transBonus:  transBonus * skill.hits,
    totalAvg,
    ampMult,
    scalingBase: scalingBase * skill.hits,
  };
}

function calcSupportBuffs(skill) {
  return {
    resReduction: skill.resReduction || 0,
    dmgBoost:     skill.dmgBoost     || 0,
    affectedHits: skill.affectedHits || 1,
  };
}

function calculateRotation(rotationData) {
  const { rotationName, enemyLevel, enemyResistance, characters } = rotationData;
  let pendingBuffs = { resReduction: 0, dmgBoost: 0, hitsRemaining: 0 };
  const charResults = [];

  for (const char of characters) {
    const skillResults = [];
    let charTotal = 0;

    for (const skill of char.skills) {
      if (skill.type === "Support") {
        const buffs = calcSupportBuffs(skill);
        pendingBuffs.resReduction  += buffs.resReduction;
        pendingBuffs.dmgBoost      += buffs.dmgBoost;
        pendingBuffs.hitsRemaining = buffs.affectedHits;

        skillResults.push({
          name: skill.name,
          type: "Support",
          resReduction: buffs.resReduction,
          dmgBoost:     buffs.dmgBoost,
          affectedHits: buffs.affectedHits,
          totalAvg: 0,
        });
        continue;
      }

      const activeBuff = pendingBuffs.hitsRemaining > 0 ? {
        resReduction: pendingBuffs.resReduction,
        dmgBoost:     pendingBuffs.dmgBoost,
      } : {};

      const result = calcAttackSkillDamage(
        skill, char.stats, enemyLevel, enemyResistance / 100, activeBuff
      );

      if (pendingBuffs.hitsRemaining > 0) {
        pendingBuffs.hitsRemaining--;
        if (pendingBuffs.hitsRemaining === 0) {
          pendingBuffs = { resReduction: 0, dmgBoost: 0, hitsRemaining: 0 };
        }
      }

      skillResults.push({
        name:         skill.name,
        type:         "Attack",
        element:      skill.element,
        hits:         skill.hits,
        ampReaction:  skill.ampReaction,
        transReaction:skill.transReaction,
        buffApplied:  Object.keys(activeBuff).length > 0,
        ...result,
      });

      charTotal += result.totalAvg;
    }

    charResults.push({
      name:         char.name,
      element:      char.stats.element,
      stats:        char.stats,
      skillResults,
      charTotal,
    });
  }

  const rotationTotal = charResults.reduce((s, c) => s + c.charTotal, 0);
  return { rotationName, enemyLevel, enemyResistance, charResults, rotationTotal };
}

// ══════════════════════════════════════════════════════════════
//  UI
// ══════════════════════════════════════════════════════════════

let characters = [];
let activeCharIdx = 0;

function showScreen(screen)
{
    document.getElementById("screenHome").style.display = "none";
    document.getElementById("screenCalculator").style.display = "none";
    document.getElementById("screenAbout").style.display = "none";

    if(screen === "home")
        document.getElementById("screenHome").style.display = "block";

    if(screen === "calculator")
        document.getElementById("screenCalculator").style.display = "block";

    if(screen === "about")
        document.getElementById("screenAbout").style.display = "block";
}

function init() {
  for (let i = 0; i < 4; i++) addCharacter(false);
  renderTabs();
  renderActiveChar();
}

function addCharacter(render) {
  if (render === undefined) render = true;
  characters.push({
    name: "Personaje " + (characters.length + 1),
    stats: {
      element: "Pyro",
      baseHP: 15000, baseATK: 2000, baseDEF: 800,
      em: 0, critRate: 70, critDmg: 140,
      elemDmgBonus: 46.6, physDmgBonus: 0,
    },
    skills: [],
  });
  activeCharIdx = characters.length - 1;
  if (render) { renderTabs(); renderActiveChar(); }
}

function renderTabs() {
  const tabs = document.getElementById('charTabs');
  let html = characters.map(function(c, i) {
    return '<button class="tab ' + (i === activeCharIdx ? 'active' : '') + '" data-idx="' + i + '">' +
      (ELEMENT_ICONS[c.stats.element] || '⚪') + ' ' + escHtml(c.name) +
    '</button>';
  }).join('');

  if (characters.length > 1) {
    html += '<button class="tab" id="btnRemoveChar" style="color:#ef4444;border-color:#ef4444">✕ Eliminar</button>';
  }

  tabs.innerHTML = html;

  // bind tab clicks
  tabs.querySelectorAll('.tab[data-idx]').forEach(function(btn) {
    btn.addEventListener('click', function() {
      activeCharIdx = parseInt(this.dataset.idx);
      renderTabs();
      renderActiveChar();
    });
  });

  var rmBtn = document.getElementById('btnRemoveChar');
  if (rmBtn) {
    rmBtn.addEventListener('click', function() {
      if (characters.length <= 1) return;
      characters.splice(activeCharIdx, 1);
      if (activeCharIdx >= characters.length) activeCharIdx = characters.length - 1;
      renderTabs();
      renderActiveChar();
    });
  }
}

function escHtml(s) {
  return String(s).replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;');
}

function renderActiveChar() {
  const c = characters[activeCharIdx];
  const container = document.getElementById('charForms');

  const elemOpts = ELEMENTS.map(function(e) {
    return '<option value="' + e + '"' + (c.stats.element === e ? ' selected' : '') + '>' + ELEMENT_ICONS[e] + ' ' + e + '</option>';
  }).join('');

  container.innerHTML =
    '<div class="grid-2">' +
      '<div class="field"><label>Nombre</label>' +
        '<input type="text" id="cName" value="' + escHtml(c.name) + '"/></div>' +
      '<div class="field"><label>Elemento</label>' +
        '<select id="cElem">' + elemOpts + '</select></div>' +
    '</div>' +

    '<div style="margin:14px 0 6px;font-size:0.72rem;color:var(--muted);text-transform:uppercase;letter-spacing:.1em;font-weight:700">Estadísticas base</div>' +
    '<div class="grid-3">' +
      '<div class="field"><label>HP Máx</label><input type="number" id="s_baseHP" value="' + c.stats.baseHP + '"/></div>' +
      '<div class="field"><label>ATK Base</label><input type="number" id="s_baseATK" value="' + c.stats.baseATK + '"/></div>' +
      '<div class="field"><label>DEF Base</label><input type="number" id="s_baseDEF" value="' + c.stats.baseDEF + '"/></div>' +
      '<div class="field"><label>Maestría Elemental (EM)</label><input type="number" id="s_em" value="' + c.stats.em + '"/></div>' +
      '<div class="field"><label>Prob. Crítico (%)</label><input type="number" id="s_critRate" value="' + c.stats.critRate + '"/></div>' +
      '<div class="field"><label>Daño Crítico (%)</label><input type="number" id="s_critDmg" value="' + c.stats.critDmg + '"/></div>' +
      '<div class="field"><label>Bonif. Daño Elemental (%)</label><input type="number" id="s_elemDmgBonus" value="' + c.stats.elemDmgBonus + '"/></div>' +
      '<div class="field"><label>Bonif. Daño Físico (%)</label><input type="number" id="s_physDmgBonus" value="' + c.stats.physDmgBonus + '"/></div>' +
    '</div>' +

    '<div style="margin:18px 0 8px;font-size:0.72rem;color:var(--muted);text-transform:uppercase;letter-spacing:.1em;font-weight:700">Habilidades en rotación</div>' +
    '<div class="skill-list" id="skillList"></div>' +
    '<button class="add-skill-btn" id="btnAddSkill">+ Agregar habilidad</button>';

  // bind name
  document.getElementById('cName').addEventListener('input', function() {
    characters[activeCharIdx].name = this.value;
    renderTabs();
  });
  // bind element
  document.getElementById('cElem').addEventListener('change', function() {
    characters[activeCharIdx].stats.element = this.value;
    renderTabs();
  });
  // bind stats
  ['baseHP','baseATK','baseDEF','em','critRate','critDmg','elemDmgBonus','physDmgBonus'].forEach(function(field) {
    var el = document.getElementById('s_' + field);
    if (el) el.addEventListener('input', function() {
      characters[activeCharIdx].stats[field] = parseFloat(this.value) || 0;
    });
  });
  // bind add skill
  document.getElementById('btnAddSkill').addEventListener('click', function() {
    characters[activeCharIdx].skills.push({
      name: 'Habilidad ' + (characters[activeCharIdx].skills.length + 1),
      type: 'Attack',
      motionValue: 250, hpScaling: 0, defScaling: 0, emScaling: 0,
      hits: 1, element: characters[activeCharIdx].stats.element,
      ampReaction: 'None', transReaction: 'None',
      resReduction: 0, dmgBoost: 0, affectedHits: 1,
    });
    renderSkills();
  });

  renderSkills();
}

function renderSkills() {
  const c = characters[activeCharIdx];
  const list = document.getElementById('skillList');
  if (!list) return;

  list.innerHTML = c.skills.map(function(sk, si) {
    const isSupport = sk.type === 'Support';
    const typeBadge = isSupport
      ? '<span class="skill-type-badge badge-support">🛡️ Soporte</span>'
      : '<span class="skill-type-badge badge-attack">⚔️ Ataque</span>';

    const elemOpts = ELEMENTS.map(function(e) {
      return '<option value="' + e + '"' + (sk.element === e ? ' selected' : '') + '>' + ELEMENT_ICONS[e] + ' ' + e + '</option>';
    }).join('');

    const ampOpts = Object.entries(AMPLIFYING_REACTIONS).map(function(kv) {
      return '<option value="' + kv[0] + '"' + (sk.ampReaction === kv[0] ? ' selected' : '') + '>' + escHtml(kv[1].label) + '</option>';
    }).join('');

    const transOpts = Object.entries(TRANSFORMATIVE_REACTIONS).map(function(kv) {
      return '<option value="' + kv[0] + '"' + (sk.transReaction === kv[0] ? ' selected' : '') + '>' + escHtml(kv[1].label) + '</option>';
    }).join('');

    const attackFields = !isSupport ? (
      '<div class="grid-3">' +
        '<div class="field"><label>Motion Value (% ATK)</label><input type="number" data-si="'+si+'" data-field="motionValue" value="'+(sk.motionValue||0)+'"/></div>' +
        '<div class="field"><label>Escala HP (%)</label><input type="number" data-si="'+si+'" data-field="hpScaling" value="'+(sk.hpScaling||0)+'"/></div>' +
        '<div class="field"><label>Escala DEF (%)</label><input type="number" data-si="'+si+'" data-field="defScaling" value="'+(sk.defScaling||0)+'"/></div>' +
        '<div class="field"><label>Escala EM (% de EM)</label><input type="number" data-si="'+si+'" data-field="emScaling" value="'+(sk.emScaling||0)+'"/></div>' +
        '<div class="field"><label>N° de hits</label><input type="number" data-si="'+si+'" data-field="hits" min="1" value="'+(sk.hits||1)+'"/></div>' +
        '<div class="field"><label>Elemento del daño</label><select data-si="'+si+'" data-field="element">'+elemOpts+'</select></div>' +
      '</div>' +
      '<div class="reaction-grid">' +
        '<div class="reaction-box">' +
          '<div class="reaction-box-title">🔥 Reacción Amplificadora<br><span style="font-weight:400;color:#999">(Melt / Vaporize)</span></div>' +
          '<select data-si="'+si+'" data-field="ampReaction">'+ampOpts+'</select>' +
          '<div class="hint" style="margin-top:6px">El bonus de EM se aplica automáticamente sobre el multiplicador (×1.5 / ×2.0).</div>' +
        '</div>' +
        '<div class="reaction-box">' +
          '<div class="reaction-box-title">⚡ Reacción Transformativa<br><span style="font-weight:400;color:#999">(Bloom, Overloaded…)</span></div>' +
          '<select data-si="'+si+'" data-field="transReaction">'+transOpts+'</select>' +
          '<div class="hint" style="margin-top:6px">Daño fijo basado en la EM del personaje, añadido al daño promedio.</div>' +
        '</div>' +
      '</div>'
    ) : '';

    const supportFields = isSupport ? (
      '<div class="support-section">' +
        '<div class="support-section-title">🛡️ Efectos de soporte (se aplican a skills siguientes)</div>' +
        '<div class="grid-3">' +
          '<div class="field"><label>Reducción de RES (%)</label>' +
            '<input type="number" data-si="'+si+'" data-field="resReductionPct" min="0" max="100" value="'+((sk.resReduction||0)*100).toFixed(0)+'"/>' +
            '<span style="font-size:.68rem;color:var(--muted)">ej: 40 → reduce RES 40%</span></div>' +
          '<div class="field"><label>Amplificación de daño (%)</label>' +
            '<input type="number" data-si="'+si+'" data-field="dmgBoost" min="0" value="'+(sk.dmgBoost||0)+'"/>' +
            '<span style="font-size:.68rem;color:var(--muted)">ej: 25 → +25% daño</span></div>' +
          '<div class="field"><label>Hits beneficiados</label>' +
            '<input type="number" data-si="'+si+'" data-field="affectedHits" min="1" value="'+(sk.affectedHits||1)+'"/>' +
            '<span style="font-size:.68rem;color:var(--muted)">cuántos skills siguientes</span></div>' +
        '</div>' +
      '</div>'
    ) : '';

    const typeOpts =
      '<option value="Attack"'  + (!isSupport ? ' selected' : '') + '>⚔️ Ataque</option>' +
      '<option value="Support"' + ( isSupport ? ' selected' : '') + '>🛡️ Soporte</option>';

    return (
      '<div class="skill-card">' +
        '<div class="skill-header">' +
          '<div class="skill-num">' + (si+1) + '</div>' +
          '<input type="text" data-si="'+si+'" data-field="name" value="'+escHtml(sk.name||'')+'" style="flex:1;max-width:200px" placeholder="Nombre habilidad"/>' +
          typeBadge +
          '<select data-si="'+si+'" data-field="type" style="font-size:.78rem;padding:4px 8px;border-radius:6px">' + typeOpts + '</select>' +
          '<button class="skill-remove" data-remove="'+si+'">✕</button>' +
        '</div>' +
        attackFields +
        supportFields +
      '</div>'
    );
  }).join('');

  // Bind all skill inputs/selects
  list.querySelectorAll('[data-si][data-field]').forEach(function(el) {
    var evt = (el.tagName === 'SELECT') ? 'change' : 'input';
    el.addEventListener(evt, function() {
      var si = parseInt(this.dataset.si);
      var field = this.dataset.field;
      var val = this.value;
      var sk = characters[activeCharIdx].skills[si];

      if (field === 'hits') {
        sk.hits = parseInt(val) || 1;
      } else if (field === 'resReductionPct') {
        sk.resReduction = (parseFloat(val) || 0) / 100;
      } else if (field === 'type') {
        sk.type = val;
        renderSkills();
      } else if (['name','element','ampReaction','transReaction'].includes(field)) {
        sk[field] = val;
      } else {
        sk[field] = parseFloat(val) || 0;
      }
    });
  });

  // Bind remove buttons
  list.querySelectorAll('[data-remove]').forEach(function(btn) {
    btn.addEventListener('click', function() {
      var si = parseInt(this.dataset.remove);
      characters[activeCharIdx].skills.splice(si, 1);
      renderSkills();
    });
  });
}

// ── Cálculo y resultados ─────────────────────────────────────
document.getElementById('btnCalc').addEventListener('click', function() {
  const rotationData = {
    rotationName: document.getElementById('rotName').value || 'Mi Rotación',
    enemyLevel:   parseInt(document.getElementById('enemyLevel').value) || 90,
    enemyResistance: parseFloat(document.getElementById('enemyRes').value) || 10,
    characters: characters,
  };
  const result = calculateRotation(rotationData);
  renderResults(result);
});

function fmt(n) {
  if (n >= 1000000) return (n/1000000).toFixed(2) + 'M';
  if (n >= 1000)    return (n/1000).toFixed(1) + 'K';
  return Math.round(n).toLocaleString();
}

function renderResults(r) {
  const el = document.getElementById('resultsContent');
  const res = document.getElementById('results');
  res.classList.add('visible');

  let html =
    '<div class="result-rotation-title">🌟 ' + escHtml(r.rotationName) + '</div>' +
    '<div class="result-meta">Nivel enemigo: <b>' + r.enemyLevel + '</b> · Resistencia base: <b>' + r.enemyResistance + '%</b></div>' +
    '<div style="height:20px"></div>';

  // Bar chart
  const max = Math.max.apply(null, r.charResults.map(function(c){ return c.charTotal; }).concat([1]));
  html += '<div class="bar-chart">';
  r.charResults.forEach(function(ch) {
    var pct = (ch.charTotal / max * 100).toFixed(1);
    var col = ELEMENT_COLORS[ch.element] || '#888';
    html +=
      '<div class="bar-row">' +
        '<div class="bar-name">' + (ELEMENT_ICONS[ch.element]||'') + ' ' + escHtml(ch.name) + '</div>' +
        '<div class="bar-track"><div class="bar-fill" style="width:'+pct+'%;background:linear-gradient(90deg,'+col+'88,'+col+')"></div></div>' +
        '<div class="bar-val">' + fmt(ch.charTotal) + '</div>' +
      '</div>';
  });
  html += '</div><div style="height:24px"></div>';

  r.charResults.forEach(function(ch) {
    var col = ELEMENT_COLORS[ch.element] || '#888';
    html += '<div class="result-char" style="border-color:'+col+'">';
    html +=
      '<div class="result-char-header">' +
        '<span class="result-char-name">' + (ELEMENT_ICONS[ch.element]||'') + ' ' + escHtml(ch.name) + '</span>' +
        '<span class="elem-badge" style="border-color:'+col+'44;color:'+col+'">' + ch.element + '</span>' +
      '</div>';
    html +=
      '<div class="result-char-stats">' +
        'ATK: <b>'+Math.round(ch.stats.baseATK)+'</b> · HP: <b>'+Math.round(ch.stats.baseHP)+'</b> · ' +
        'DEF: <b>'+Math.round(ch.stats.baseDEF)+'</b> · EM: <b>'+Math.round(ch.stats.em)+'</b><br>' +
        'CRIT: <b>'+ch.stats.critRate+'%</b> / <b>'+ch.stats.critDmg+'%</b> · Bonif. Elem: <b>'+ch.stats.elemDmgBonus+'%</b>' +
      '</div>';

    ch.skillResults.forEach(function(sk) {
      if (sk.type === 'Support') {
        html +=
          '<div class="result-support-skill">' +
            '🛡️ <b>' + escHtml(sk.name) + '</b> — Soporte activo: ' +
            (sk.resReduction > 0 ? '<span>▼ RES −'+(sk.resReduction*100).toFixed(0)+'%</span>' : '') +
            (sk.dmgBoost > 0 ? '<span>  ▲ Daño +'+sk.dmgBoost+'%</span>' : '') +
            '<span style="opacity:.7"> (próximos '+sk.affectedHits+' skill'+(sk.affectedHits>1?'s':'')+')</span>' +
          '</div>';
        return;
      }

      var ampR   = AMPLIFYING_REACTIONS[sk.ampReaction];
      var transR = TRANSFORMATIVE_REACTIONS[sk.transReaction];
      var hasAmp   = sk.ampReaction   && sk.ampReaction   !== 'None';
      var hasTrans = sk.transReaction && sk.transReaction !== 'None';

      html +=
        '<div class="result-skill">' +
          '<div class="result-skill-name">' +
            '<span>' + (ELEMENT_ICONS[sk.element]||'') + '</span>' +
            '<b>' + escHtml(sk.name) + '</b>' +
            (sk.buffApplied ? '<span class="buff-pill">🛡️ Buff activo</span>' : '') +
            (hasAmp   ? '<span class="amp-pill">' + escHtml(ampR.label) + ' ×' + (sk.ampMult||1).toFixed(2) + '</span>' : '') +
            (hasTrans ? '<span class="amp-pill" style="background:rgba(91,141,238,.12);color:#93c5fd;border-color:rgba(91,141,238,.25)">' + escHtml(transR.label) + '</span>' : '') +
          '</div>' +
          '<div class="result-nums">' +
            '<div class="num-block"><div class="num-label">Hits</div><div class="num-value">'+sk.hits+'</div></div>' +
            '<div class="num-block"><div class="num-label">Normal (total)</div><div class="num-value">'+fmt(sk.baseDmg)+'</div></div>' +
            '<div class="num-block"><div class="num-label">Crítico (total)</div><div class="num-value crit">'+fmt(sk.critDmg)+'</div></div>' +
            '<div class="num-block"><div class="num-label">Promedio</div><div class="num-value avg">'+fmt(sk.avgDmg)+'</div></div>' +
            (hasTrans ? '<div class="num-block"><div class="num-label">Bono Reacción</div><div class="num-value" style="color:#93c5fd">+'+fmt(sk.transBonus)+'</div></div>' : '') +
            '<div class="num-block"><div class="num-label">▶ Total Promedio</div><div class="num-value total">'+fmt(sk.totalAvg)+'</div></div>' +
          '</div>' +
        '</div>';
    });

    html +=
      '<div class="char-total-row">Total ' + escHtml(ch.name) + ': <span class="char-total-val">' + fmt(ch.charTotal) + '</span></div>' +
      '</div>';
  });

  html +=
    '<div class="rotation-total">' +
      '<div class="rotation-total-label">💥 Daño Total de la Rotación</div>' +
      '<div class="rotation-total-val">' + fmt(r.rotationTotal) + '</div>' +
    '</div>' +
    '<div class="hint" style="margin-top:14px">' +
      '<strong>Melt Fuerte / Vaporize Fuerte ×2.0:</strong> Pyro sobre Cryo (Melt) · Hydro sobre Pyro (Vaporize)<br>' +
      '<strong>Melt Débil / Vaporize Débil ×1.5:</strong> Cryo sobre Pyro (Melt) · Pyro sobre Hydro (Vaporize)<br>' +
      'El bonus de EM se aplica en ambos tipos de reacciones. Las skills de <strong>Soporte</strong> reducen la RES del enemigo y amplifican el daño de las skills siguientes.' +
    '</div>';

  el.innerHTML = html;
  res.scrollIntoView({ behavior: 'smooth', block: 'start' });
}

// ── Botón agregar personaje ──────────────────────────────────
document.getElementById('btnAddChar').addEventListener('click', function() {
  addCharacter(true);
});

// ── Arrancar ─────────────────────────────────────────────────
init();