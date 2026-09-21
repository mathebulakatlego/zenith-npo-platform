import { NavLink } from 'react-router-dom'

function Navbar() {
  return (
    <header className="site-header">
      <nav className="navbar" aria-label="Main navigation">
        <NavLink className="navbar__brand" to="/home">
          Zenith Future Skills Hub
        </NavLink>

        <div className="navbar__links">
          <NavLink to="/home">Home</NavLink>
          <NavLink to="/about">About</NavLink>
          <NavLink to="/programmes">Programmes</NavLink>
          <NavLink to="/contact">Contact</NavLink>
          <NavLink to="/support">Support</NavLink>
        </div>

        <NavLink className="btn btn--primary navbar__cta" to="/apply">
          Apply Now
        </NavLink>
      </nav>
    </header>
  );
}

export default Navbar;