function Footer() {
  return (
    <footer className="site-footer">
      <div className="page-container site-footer__content">
        <div className="site-footer__brand-column">
          <a className="site-footer__brand" href="/">
            Zenith Future Skills Hub
          </a>

          <div className="site-footer__socials" aria-label="Social media">
            <a
              href="https://www.linkedin.com/company/zenith-future-skills-hub/"
              target="_blank"
              rel="noreferrer"
              aria-label="LinkedIn"
            >
               <svg viewBox="0 0 24 24" aria-hidden="true">
                <path d="M6.5 8.5H3V21h3.5V8.5ZM4.75 3A2.05 2.05 0 1 0 4.75 7.1 2.05 2.05 0 0 0 4.75 3ZM21 13.75c0-3.76-2.01-5.51-4.7-5.51-2.17 0-3.14 1.2-3.68 2.04V8.5H9.12V21h3.5v-6.19c0-1.63.31-3.2 2.33-3.2 1.99 0 2.02 1.85 2.02 3.31V21H21v-7.25Z" />
              </svg>
            </a>

            <a href="https://www.facebook.com/people/Zenith-Future-Skills-Hub/61592400982295/#" aria-label="Facebook">
            <svg viewBox="0 0 24 24" aria-hidden="true">
              <path d="M14 8h3V4.5h-3c-3.31 0-5 1.69-5 5V12H6v3.5h3V21h3.5v-5.5H16L16.5 12h-4v-2.5c0-1 .5-1.5 1.5-1.5Z" />
            </svg>
            </a>

            <a href="https://www.instagram.com/zeniith_future_skills_hub/" aria-label="Instagram">
             <svg viewBox="0 0 24 24" aria-hidden="true">
              <rect x="3.5" y="3.5" width="17" height="17" rx="5" />
              <circle cx="12" cy="12" r="4" />
              <circle cx="17.5" cy="6.5" r="1" className="social-icon__dot" />
            </svg>
            </a>

            <a href="#" aria-label="TikTok">
             <svg viewBox="0 0 24 24" aria-hidden="true">
              <path d="M15 3h3.2c.35 1.7 1.4 3.08 3 3.8V10c-1.15-.08-2.2-.4-3.2-.98V15a6 6 0 1 1-6-6v3.2a2.8 2.8 0 1 0 2.8 2.8V3.8c.07-.25.13-.52.2-.8Z" />
            </svg>
            </a>
          </div>
        </div>

        <div className="site-footer__column">
          <h2>Quick Links</h2>

          <a href="/">Home</a>
          <a href="/about">About</a>
          <a href="/programmes">Programmes</a>
          <a href="/contact">Contact</a>
          <a href="/support">Support</a>
        </div>

        <div className="site-footer__column">
          <h2>Contact</h2>

          <a href="mailto:info@zenithfutureskills.co.za">
            info@zenithfutureskills.co.za
          </a>

          <a href="tel:+27785310038">
            078 531 0038
          </a>

          <span>Johannesburg, South Africa</span>
        </div>
      </div>

      <div className="page-container site-footer__bottom">
        <p>© 2026 Zenith Future Skills Hub. All rights reserved.</p>
      </div>
    </footer>
  );
}

export default Footer;