function Footer() {
  return (
    <footer className="site-footer">
      <div className="page-container site-footer__content">
        <div>
          <a className="site-footer__brand" href="/">
            Zenith Future Skills Hub
          </a>
          <p className="site-footer__tagline">
            Skill | Grow | Lead
          </p>
        </div>

        <div className="site-footer__links">
          <a href="/about">About</a>
          <a href="/programmes">Programmes</a>
          <a href="/contact">Contact</a>
        </div>
      </div>

      <div className="page-container site-footer__bottom">
        <p>© 2026 Zenith Future Skills Hub. All rights reserved.</p>
      </div>
    </footer>
  );
}

export default Footer;
