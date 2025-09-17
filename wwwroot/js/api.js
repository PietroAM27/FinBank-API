// === CONFIG ===
// Using same origin (no need to specify base URL).
// If you prefer, you can keep a custom base like: const BASE_URL = 'http://localhost:5245';
const BASE_URL = '';

// === UTILS ===
function setToken(token){ localStorage.setItem('jwt', token); }
function getToken(){ return localStorage.getItem('jwt'); }
function isLogged(){ return !!getToken(); }
function logout(){ localStorage.removeItem('jwt'); window.location.href = '/index.html'; }

async function api(path, { method='GET', body=null, auth=true } = {}){
  const headers = { 'Content-Type': 'application/json' };
  if(auth && getToken()) headers['Authorization'] = 'Bearer ' + getToken();
  const res = await fetch(`${BASE_URL}${path}`, { method, headers, body: body?JSON.stringify(body):null });
  if(!res.ok){
    const msg = await res.text();
    throw new Error(`${res.status} ${res.statusText} — ${msg}`);
  }
  const ct = res.headers.get('content-type')||'';
  if(ct.includes('application/json')) return await res.json();
  return await res.text();
}

function requireAuth(){
  if(!isLogged()) { window.location.href = '/index.html'; }
}
