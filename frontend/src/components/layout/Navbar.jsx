function Navbar() {
  return (
    <header className="site-header">
      <nav className="navbar" aria-label="Main navigation">
        <a className="navbar__brand" href="/">
          Zenith Future Skills Hub
        </a>

        <div className="navbar__links">
          <a href="/about">About</a>
          <a href="/programmes">Programmes</a>
          <a href="/team">Team</a>
          <a href="/contact">Contact</a>
        </div>

        <a className="btn btn--primary navbar__cta" href="/apply">
          Apply Now
        </a>
      </nav>
    </header>
  );
}

export default Navbar;