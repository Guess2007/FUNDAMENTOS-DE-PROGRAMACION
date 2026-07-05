// ══════════════════════════════════════════════════════════════
//  Manejo de sesión (login/registro) contra el backend Express
// ══════════════════════════════════════════════════════════════

const AUTH_TOKEN_KEY = 'authToken';
const AUTH_USER_KEY  = 'authUser';

function getToken()    { return localStorage.getItem(AUTH_TOKEN_KEY); }
function getUsername() { return localStorage.getItem(AUTH_USER_KEY); }

function saveSession(token, username) {
  localStorage.setItem(AUTH_TOKEN_KEY, token);
  localStorage.setItem(AUTH_USER_KEY, username);
}

function clearSession() {
  localStorage.removeItem(AUTH_TOKEN_KEY);
  localStorage.removeItem(AUTH_USER_KEY);
}

// Pregunta al backend si el token guardado sigue siendo válido
async function checkAuth() {
  const token = getToken();
  if (!token) return { loggedIn: false };

  try {
    const res = await fetch('/api/me', {
      headers: { Authorization: 'Bearer ' + token }
    });
    if (!res.ok) {
      clearSession();
      return { loggedIn: false };
    }
    return await res.json();
  } catch (err) {
    return { loggedIn: false };
  }
}

// Llamar al inicio de cualquier página que deba estar protegida.
// Si no hay sesión válida, manda a login.html
async function requireAuth() {
  const status = await checkAuth();
  if (!status.loggedIn) {
    window.location.href = 'login.html';
  }
}

function logout() {
  clearSession();
  window.location.href = 'login.html';
}

// Actualiza el hueco #authNavSlot del navbar con "Login" o "Cerrar sesión (usuario)".
// main.js la llama justo después de inyectar navbar.html
async function updateNavbarAuth() {
  const slot = document.getElementById('authNavSlot');
  if (!slot) return;

  const status = await checkAuth();

  if (status.loggedIn) {
    slot.innerHTML = '<a href="#" class="nav-link" id="logoutLink">Cerrar sesión (' + status.username + ')</a>';
    document.getElementById('logoutLink').addEventListener('click', function (e) {
      e.preventDefault();
      logout();
    });
  } else {
    slot.innerHTML = '<a href="login.html" class="nav-link">Login</a>';
  }
}
