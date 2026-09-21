import PageContainer from '../components/layout/PageContainer'
import Section from '../components/layout/Section'

function Contact() {
  return (
    <main>
      <section className="page-hero">
        <PageContainer>
          <div className="page-hero__content">
            <p className="section-eyebrow">Get In Touch</p>
            <h1>Contact Zenith</h1>
            <p>
              We're here to answer your questions and connect you with the
              right person at Zenith.
            </p>
          </div>
        </PageContainer>
      </section>

      <Section>
        <PageContainer>
          <div className="contact-form-layout">
            <div>
              <p className="section-eyebrow">Send Us A Message</p>
              <h2>Let's connect.</h2>
              <p>
                Have a question about our programmes, applications,
                partnerships, or how you can get involved? Send us a message
                and we'll get back to you.
              </p>

              <div className="contact-details">
                <div className="contact-detail">
                  <span>Email</span>
                  <a href="mailto:info@zenithfutureskills.co.za">
                    info@zenithfutureskills.co.za
                  </a>
                </div>

                <div className="contact-detail">
                  <span>Phone</span>
                  <a href="tel:+27785310038">078 531 0038</a>
                </div>

                <div className="contact-detail">
                  <span>Location</span>
                  <span>Johannesburg, South Africa</span>
                </div>
              </div>
            </div>

            <form className="contact-form">
              <div className="form-field">
                <label htmlFor="name">Name</label>
                <input id="name" name="name" type="text" required />
              </div>

              <div className="form-field">
                <label htmlFor="email">Email</label>
                <input id="email" name="email" type="email" required />
              </div>

              <div className="form-field">
                <label htmlFor="subject">Subject</label>
                <select id="subject" name="subject" defaultValue="" required>
                  <option value="" disabled>
                    Select a subject
                  </option>
                  <option value="programme-enquiry">Programme enquiry</option>
                  <option value="application-support">
                    Application support
                  </option>
                  <option value="partnership">Partnership</option>
                  <option value="volunteering">Volunteering</option>
                  <option value="supporting-zenith">Supporting Zenith</option>
                  <option value="general-enquiry">General enquiry</option>
                </select>
              </div>

              <div className="form-field">
                <label htmlFor="message">Message</label>
                <textarea id="message" name="message" rows="6" required />
              </div>

              <button className="btn btn--primary" type="submit">
                Send Message
              </button>
            </form>
          </div>
        </PageContainer>
      </Section>

      <Section className="section--light">
        <PageContainer>
          <div className="contact-support">
            <p className="section-eyebrow">Support Zenith</p>
            <h2>Help us create opportunities.</h2>
            <p className="contact-support__intro">
              Your support helps us create access to technology, practical
              skills, and career readiness for young people.
            </p>

            <div className="contact-support__items">
              <article>
                <h3>Volunteer</h3>
                <p>
                  Share your time and skills. Mentor, facilitate, or support
                  learners on their journey.
                </p>
                <a href="/support">Get involved →</a>
              </article>

              <article>
                <h3>Partner</h3>
                <p>
                  Collaborate with us to create opportunities, resources, and
                  shared impact.
                </p>
                <a href="/support">Partner with us →</a>
              </article>

              <article>
                <h3>Support Our Work</h3>
                <p>
                  Provide resources and support that help learners access
                  meaningful skills and opportunities.
                </p>
                <a href="/support">Support our work →</a>
              </article>
            </div>
          </div>
        </PageContainer>
      </Section>

      <section className="contact-cta">
        <PageContainer>
          <div className="contact-cta__content">
            <p className="section-eyebrow">Support Digital Opportunity</p>
            <h2>Let's create opportunities together.</h2>
            <p>
              Get in touch, support our work, or find out more about our
              programmes.
            </p>

            <div className="contact-cta__actions">
              <a className="btn btn--primary" href="/contact">
                Contact Us
              </a>
              <a className="btn btn--secondary" href="/support">
                Support Our Work
              </a>
            </div>
          </div>
        </PageContainer>
      </section>
    </main>
  )
}

export default Contact
